using System;

namespace Foole.WC3Proxy.Warcraft3
{
    public struct GameInfo
    {
        public int GameId;
        public string Name;
        public string Map;
        public int Port;
        public int SlotCount;
        public int CurrentPlayers;
        public int PlayerSlots;

        public GameInfo(int gameId, string name, string map, ushort port, int slotCount, int currentPlayers, int playerSlots)
        {
            GameId = gameId;
            Name = name;
            Map = map;
            Port = port;
            SlotCount = slotCount;
            CurrentPlayers = currentPlayers;
            PlayerSlots = playerSlots;
        }
    }
}
