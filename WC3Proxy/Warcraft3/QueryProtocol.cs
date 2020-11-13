using System;
using System.IO;
using System.Text;

namespace Foole.WC3Proxy.Warcraft3
{
    static class QueryProtocol
    {
        public static byte[] GetGameCancelledPacket(int gameId)
        {
            return new byte[] { 0xf7, 0x33, 0x08, 0x00, (byte)gameId, 0x00, 0x00, 0x00 };
        }

        public static byte[] GetGameAnnouncePacket(GameInfo game)
        {
            int players = game.SlotCount - game.PlayerSlots + game.CurrentPlayers;
            return new byte[] { 0xf7, 0x32, 0x10, 0x00, (byte)game.GameId, 0x00, 0x00, 0x00, (byte)players, 0, 0, 0, (byte)game.SlotCount, 0, 0, 0 };
        }

        public static byte[] GetBrowsePacket(bool expansion, byte version)
        {
            if (expansion) // TFT - PX3W instead of 3RAW
                return new byte[] { 0xf7, 0x2f, 0x10, 0x00, 0x50, 0x58, 0x33, 0x57, version, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
            else // ROC
                return new byte[] { 0xf7, 0x2f, 0x10, 0x00, 0x33, 0x52, 0x41, 0x57, version, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
        }

        public static GameInfo? ExtractGameInfo(byte[] response, int length)
        {
            if (response[0] != 0xf7 || response[1] != 0x30) return null;

            var gameId = BitConverter.ToInt32(response, 0xc);
            var name = StringFromArray(response, 0x14);

            int cryptstart = 0x14 + name.Length + 1 + 1; // one extra byte after the server name
            byte[] decrypted = Decrypt(response, cryptstart);
            var map = StringFromArray(decrypted, 0xd);

            var port = BitConverter.ToUInt16(response, length - 2);
            var slotCount = BitConverter.ToInt32(response, length - 22);
            var currentPlayers = BitConverter.ToInt32(response, length - 14);
            var playerSlots = BitConverter.ToInt32(response, length - 10);

            return new GameInfo(gameId, name, map, port, slotCount, currentPlayers, playerSlots);
        }

        static string StringFromArray(byte[] data, int offset)
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

        //This is also used to decrypt recorded game file headers
        static byte[] Decrypt(byte[] Data, int Offset)
        {
            // TODO: calculate the real result length (Data.Length * 8 / 9?).
            // in=37, out=30.  in=3a, out=32.
            MemoryStream output = new MemoryStream();
            int pos = 0;
            byte mask = 0;
            while (true)
            {
                byte b = Data[pos + Offset];
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
    }
}
