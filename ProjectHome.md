# Overview #

This program allows you to play Warcraft 3 multiplayer over the internet without using Battle.Net.  It does this by forwarding data between your network and the host so that they appear to be on the same LAN.

It is based on a similar program called LanCraft.  (Thanks to the creators of LanCraft: ttol and Coolest.  I have been using it for years.)

# Features #

  * Displays game details (Name, map, players etc)
  * Can act as a proxy for multiple clients on a LAN.
  * Has a system tray icon that shows popups when games are found.
  * Much easier to setup/use than pvpgn etc. for quick games.
  * Proxied games show as "Proxy Game" instead of "Local Game" in Warcraft 3.
  * Does not require fiddling with port numbers
  * Remembers the server you last used

# Usage #

  * The host hosts a LAN game in Warcraft 3.  Port 6112 must be accessible to the other players.
  * Everyone else runs WC3Proxy and enters the host's IP address in the "Server information" dialog.  Tick the box for Frozen Throne if applicable.  The default is Reign of Chaos.
  * Players then launch Warcraft 3 and choose the "Local Area Network" option.  The host's game should appear in the game list.

# Notes #

The game information will disappear when the game is started.

The three player counts are: current human players, total slots for human players and total slots.  The second number includes any player slots which are not closed and not AI players.  This means it also includes slots which are currently occupied by human players.  Warcraft 3 displays the player count as: total slots - human slots + human players.

Unfortunately this program is limited in 2 ways.  It will only work with WC3 versions 1.21 to 1.26, and it can only scan for either RoC or TFT at one time.  If a new patch is released for WC3 that makes this program stop working, I will release an updated version.

In theory this program should work on a Mac or Linux computer with a recent version of mono installed.  If anyone tries this, let me know how it goes.

I will consider patches, contributions etc.

Tools used to create this program: Wireshark, Paint.NET, Visual C# 2005 Express Edition.

For more WC3 related tools and info, see [the official LanCraft site](http://www.lancraftwc3.com/).