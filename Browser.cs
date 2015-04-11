/*
Copyright (c) 2008 Foole

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
 */
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Timers;

namespace Foole.WC3Proxy
{
    struct GameInfo
    {
        public int GameId;
        public string Name;
        public string Map;
        public int Port;
        public int SlotCount;
        public int CurrentPlayers;
        public int PlayerSlots;
    }

    delegate void FoundServerHandler(GameInfo server);

    sealed class Browser
    {
        Socket _browseSocket;
        byte[] _browsePacket;
        IPEndPoint _serverEP;
        IPEndPoint _clientEP = new IPEndPoint(IPAddress.Broadcast, 6112);
        Timer _queryTimer;
        bool _querying;
        byte[] _buffer = new byte[512];
        bool _expansion;
        int _proxyPort;
        byte _version; // 1.22 = 0x16, 1.21 = 0x15

        public event FoundServerHandler FoundServer;
        public event Action QuerySent;

        public Browser(IPAddress serverAddress, int proxyPort, byte version, bool expansion)
        {
            _proxyPort = proxyPort;
            _version = version;
            _expansion = expansion;
            _queryTimer = new Timer(1000);
            _queryTimer.AutoReset = true;
            _queryTimer.Elapsed += new ElapsedEventHandler(QueryTimer_Elapsed);
            // WC3 always listens on UDP 6112
            _serverEP = new IPEndPoint(serverAddress, 6112);
        }

        public byte Version
        {
            get { return _version; }
            set
            {
                _version = value;
                UpdateBrowsePacket();
            }
        }

        public bool Expansion
        {
            get { return _expansion; }
            set
            {
                _expansion = value;
                UpdateBrowsePacket();
            }
        }

        public IPAddress ServerAddress
        {
            get { return _serverEP.Address; }
            set { _serverEP.Address = value; }
        }

        public void Run()
        {
            UpdateBrowsePacket();

            _browseSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _browseSocket.Bind(new IPEndPoint(IPAddress.Any, 0));
            _browseSocket.EnableBroadcast = true;

            _queryTimer.Start();
        }

        public void Stop()
        {
            _queryTimer.Stop();
            _browseSocket.Close();
            _browseSocket = null;
        }

        void QueryTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            ProcessResponses();

            if (_querying) return;
            _querying = true;

            SendQuery();

            _querying = false;

            ProcessResponses();
        }

        public void SendQuery()
        {
            _browseSocket.SendTo(_browsePacket, _serverEP);
            if (QuerySent != null) QuerySent();
        }

        public bool ProcessResponses()
        {
            bool receivedany = false;

            while (_browseSocket.Poll(0, SelectMode.SelectRead))
            {
                int len = 0;
                try
                {
                    len = _browseSocket.Receive(_buffer);
                }
                catch (SocketException)
                {
                    // "An existing connection was forcibly closed by the remote host"
                    break;
                }
                if (len == 0) break;

                if (ExtractGameInfo(_buffer, len) == false) continue;

                receivedany = true;
                ModifyGameName(_buffer);
                ModifyGamePort(_buffer, len, _proxyPort);
                _browseSocket.SendTo(_buffer, len, SocketFlags.None, _clientEP);
            }

            return receivedany;
        }

        // Extracts the server's details from the query response and raises an event for it
        bool ExtractGameInfo(byte[] response, int Length)
        {
            if (response[0] != 0xf7 || response[1] != 0x30) return false;

            GameInfo game = new GameInfo();

            game.GameId = BitConverter.ToInt32(response, 0xc);
            game.Name = StringFromArray(response, 0x14);

            int cryptstart = 0x14 + game.Name.Length + 1 + 1; // one extra byte after the server name
            byte[] decrypted = Decrypt(response, cryptstart);
            game.Map = StringFromArray(decrypted, 0xd);

            game.Port = BitConverter.ToUInt16(response, Length - 2);
            game.SlotCount = BitConverter.ToInt32(response, Length - 22);
            game.CurrentPlayers = BitConverter.ToInt32(response, Length - 14);
            game.PlayerSlots = BitConverter.ToInt32(response, Length - 10);

            if (FoundServer != null) FoundServer(game);
            SendGameAnnounce(game);

            return true;
        }

        public void SendGameCancelled(int gameId)
        {
            byte[] packet = new byte[] { 0xf7, 0x33, 0x08, 0x00, (byte)gameId, 0x00, 0x00, 0x00 };
            _browseSocket.SendTo(packet, _clientEP);
        }

        // The client wont update the player count unless this is sent
        public void SendGameAnnounce(GameInfo gameInfo)
        {
            int players = gameInfo.SlotCount - gameInfo.PlayerSlots + gameInfo.CurrentPlayers;
            byte[] packet = new byte[] { 0xf7, 0x32, 0x10, 0x00, (byte)gameInfo.GameId, 0x00, 0x00, 0x00, (byte)players, 0, 0, 0, (byte)gameInfo.SlotCount, 0, 0, 0 };
            _browseSocket.SendTo(packet, _clientEP);
        }

        //This is also used to decrypt recorded game file headers
        byte[] Decrypt(byte[] data, int offset)
        {
            // TODO: calculate the real result length (Data.Length * 8 / 9?).
            // in=37, out=30.  in=3a, out=32.
            MemoryStream output = new MemoryStream();
            int pos = 0;
            byte mask = 0;
            while (true)
            {
                byte b = data[pos + offset];
                if (b == 0) break;
                if (pos % 8 == 0)
                {
                    mask = b;
                }
                else
                {
                    if ((mask & (0x1 << (pos % 8))) == 0)
                        output.WriteByte((byte)(b - 1));
                    else
                        output.WriteByte(b);
                }
                pos++;
            }
            return output.ToArray();
        }

        string StringFromArray(byte[] data, int offset)
        {
            StringBuilder sb = new StringBuilder();
            while (true)
            {
                char c = (char)data[offset++];
                if (c == 0) break;
                sb.Append(c);
            }
            return sb.ToString();
        }

        // Replace "Local Game" with "Proxy Game"
        // This will not work properly for other languages
        void ModifyGameName(byte[] response)
        {
            response[0x14] = (byte)'P';
            response[0x15] = (byte)'r';
            response[0x16] = (byte)'o';
            response[0x17] = (byte)'x';
            response[0x18] = (byte)'y';
        }

        void ModifyGamePort(byte[] response, int length, int port)
        {
            int index = length - 2;
            response[index] = (byte)(port & 0xff);
            response[index + 1] = (byte)(port >> 8);
        }

        // Dummy version with hard coded query packet
        void UpdateBrowsePacket()
        {
            if (_expansion) // TFT - PX3W instead of 3RAW
                _browsePacket = new byte[] { 0xf7, 0x2f, 0x10, 0x00, 0x50, 0x58, 0x33, 0x57, _version, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            else // ROC
                _browsePacket = new byte[] { 0xf7, 0x2f, 0x10, 0x00, 0x33, 0x52, 0x41, 0x57, _version, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }
    }
}
