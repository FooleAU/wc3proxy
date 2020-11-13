using System;

namespace Foole.WC3Proxy.Warcraft3
{
    public struct GameInfo
    {
        public readonly int GameId;
        public readonly string Name;
        public readonly string Map;
        public readonly int Port;
        public readonly int SlotCount;
        public readonly int CurrentPlayers;
        public readonly int PlayerSlots;

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
