When the program starts up (MainForm.MainForm\_Shown), 2 services are created:
  * Listener which starts a listening TCP socket.  The port number is assigned by the OS.
  * Browser which sends game query packets to the given WC3 server address.

The browser sends WC3 query packets to the given WC3 server address. (Browser.SendQuery).  When it receives a response (Browser.ProcessResponses), it sends a "game announce" packet (Browser.SendGameAnnounce), then modifies the response (Browser.ModifyGameName and Browser.ModifyGamePort) and broadcasts it on the local network.  It changes the game port to be the one that the Listener is listening on.

When the Listener gets a connection, a Proxy object is created (MainForm.GotConnection).  The proxy blindly forwards data between the client and the server (TcpProxy.ThreadFunc).

If no response is received in 3 seconds, a "game cancelled" message is sent to the local network.  The server sends a game cancelled message, but only to its local network, so it is not received by the proxy.