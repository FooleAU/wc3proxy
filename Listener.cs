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
using System.Net;
using System.Net.Sockets;

namespace Foole.Net
{
    delegate void GotConnectionDelegate(Socket ClientSocket);

    sealed class Listener
    {
        Socket _listenSocket;
        readonly GotConnectionDelegate _connectionHandler;
        readonly IPAddress _address;
        int _port;

        bool _stop;
        readonly AsyncCallback _acceptCallback;

        public Listener(IPAddress address, int port, GotConnectionDelegate connectionHandler)
        {
            _address = address;
            _port = port;
            _connectionHandler = connectionHandler;
            _acceptCallback = new AsyncCallback(EndAccept);
        }

        public Listener(int port, GotConnectionDelegate connectionHandler)
            : this(IPAddress.Any, port, connectionHandler)
        {
        }

        public Listener(GotConnectionDelegate connectionHandler)
            : this(IPAddress.Any, 0, connectionHandler)
        {
        }

        public void Run()
        {
            _stop = false;

            IPEndPoint EndPoint = new IPEndPoint(_address, _port);

            _listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _listenSocket.Bind(EndPoint);

            _port = LocalEndPoint.Port;
            _listenSocket.Listen(20);

            BeginAccept();
        }

        void BeginAccept()
        {
            if (_stop) return;
            _listenSocket.BeginAccept(_acceptCallback, null);
        }

        void EndAccept(IAsyncResult result)
        {
            if (_stop) return;

            try
            {
                Socket Client = _listenSocket.EndAccept(result);
                _connectionHandler(Client);
            }
            catch (ObjectDisposedException)
            {
                // Occasionally throws: System.ObjectDisposedException: Cannot access a disposed object.
                // Do nothing
            }
            BeginAccept();
        }

        public void Stop()
        {
            _stop = true;
            _listenSocket.Close();
            _listenSocket = null;
        }

        public IPEndPoint LocalEndPoint
        {
            get
            {
                return (IPEndPoint)_listenSocket.LocalEndPoint;
            }
        }
    }
}
