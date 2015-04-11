All numbers are little-endian.

# Query #

See: Browser.UpdateBrowsePacket

  * Header (00): 0xf7, 0x2f, 0x10, 0x00,
  * Game (04): 4 character game id. "WAR3" (0x33, 0x52, 0x41, 0x57) or "W3XP" (0x50, 0x58, 0x33, 0x57) in reverse order.
  * Protocol version (08): 4 byte int. Usually goes up by 1 for each patch.  0x15 for v1.21.
  * Unknown (0C): 0x00, 0x00, 0x00, 0x00

# Response #

See: Browser.ExtractGameInfo

  * Header (00): 0xf7 0x30
  * Game Id (0C): 4 bytes.  I think this goes up by one each time the host creates a new game.
  * Game name (14): Null terminated string
  * Unknown (+1)

The rest of the packet is [obfuscated](DeobfuscationAlgorithm.md).
Inside the obfuscated block:
  * Map name (0D) Null terminated string
  * Slot count (length - 22): 4 byte int
  * Current players (length - 14): 4 byte int
  * Player slots (length - 10): 4 byte int
  * Port number (length - 2): 2 byte int

# Game cancelled #

See: Browser.SendGameCancelled

  * Header (00): 0xf7, 0x33
  * Unknown (02): 0x08, 0x00
  * Game id (04): 4 byte int

# Game announce #

See: Browser.SendGameAnnounce

  * Header (00): 0xf7, 0x32
  * Unknown (02): 0x10, 0x00
  * Game Id (04): 4 byte int
  * Player count (08): 4 byte int
  * Slot count (0C): 4 byte int