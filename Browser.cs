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
using Foole.WC3Proxy.Warcraft3;

namespace Foole.WC3Proxy
{
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

        void SendQuery()
        {
            _browseSocket.SendTo(_browsePacket, _serverEP);
            if (QuerySent != null) QuerySent();
        }

        bool ProcessResponses()
        {
            bool receivedany = false;

            while (_browseSocket.Poll(0, SelectMode.SelectRead))
            {
                int len;
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

                var gameInfo = QueryProtocol.ExtractGameInfo(_buffer, len);
                if (gameInfo == null)
                    continue;

                OnFoundServer(gameInfo.Value);

                receivedany = true;
                ModifyGameName(_buffer);
                ModifyGamePort(_buffer, len, _proxyPort);
                _browseSocket.SendTo(_buffer, len, SocketFlags.None, _clientEP);
            }

            return receivedany;
        }

        public void SendGameCancelled(int gameId)
        {
            byte[] packet = QueryProtocol.GetGameCancelledPacket(gameId);
            _browseSocket.SendTo(packet, _clientEP);
        }

        // The client wont update the player count unless this is sent
        void SendGameAnnounce(GameInfo gameInfo)
        {
            byte[] packet = QueryProtocol.GetGameAnnouncePacket(gameInfo);
            _browseSocket.SendTo(packet, _clientEP);
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
            response[length - 2] = (byte)(port & 0xff);
            response[length - 1] = (byte)(port >> 8);
        }

        void UpdateBrowsePacket()
        {
            _browsePacket = QueryProtocol.GetBrowsePacket(_expansion, _version);
        }

        void OnFoundServer(GameInfo gameInfo)
        {
            if (FoundServer != null)
                FoundServer(gameInfo);

            SendGameAnnounce(gameInfo);
        }
    }
}
