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
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Foole.WC3Proxy
{
    delegate void ProxyDisconnectedHandler(TcpProxy Proxy);

    sealed class TcpProxy
    {
        Socket _clientSocket;
        Socket _serverSocket;
        EndPoint _serverEP;
        Thread _thread;
        bool _running;
        byte[] _buffer = new byte[2048];

        public event ProxyDisconnectedHandler ProxyDisconnected;

        public TcpProxy(Socket clientSocket, EndPoint serverEP)
        {
            _clientSocket = clientSocket;
            _serverEP = serverEP;

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Run()
        {
            _serverSocket.Connect(_serverEP);

            _running = true;
            _thread = new Thread(new ThreadStart(ThreadFunc));
            _thread.Start();
        }

        public void Stop()
        {
            _running = false;
            if (_thread != null) _thread.Join();
        }

        void ThreadFunc()
        {
            ArrayList sockets = new ArrayList(2);
            sockets.Add(_clientSocket);
            sockets.Add(_serverSocket);

            while (_running)
            {
                IList readsockets = (IList)sockets.Clone();
                Socket.Select(readsockets, null, null, 1000000);
                foreach (Socket s in readsockets)
                {
                    int length = 0;
                    try
                    {
                        length = s.Receive(_buffer);
                    }
                    catch { }
                    if (length == 0)
                    {
                        _running = false;
                        if (ProxyDisconnected != null) ProxyDisconnected(this);
                        break;
                    }
                    Socket dest = (s == _serverSocket) ? _clientSocket : _serverSocket;
                    dest.Send(_buffer, length, SocketFlags.None);
                }
            }
        }
    }
}
