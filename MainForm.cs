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
using System.Collections.Generic;
using System.Diagnostics; // for Process
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Foole.WC3Proxy.Net;
using Foole.WC3Proxy.Warcraft3;

namespace Foole.WC3Proxy
{
    delegate void Action();

    sealed partial class MainForm : Form
    {
        Listener _listener; // This waits for proxy connections
        Browser _browser; // This sends game info queries to the server and forwards the responses to the client
        readonly List<TcpProxy> _proxies = new List<TcpProxy>(); // A collection of game proxies.  Usually we would only need 1 proxy.

        readonly ServerInfo _serverInfo;
        IPHostEntry _serverHost;
        IPEndPoint _serverEP;

        // TODO: Possibly move these (and associated code) into the Browser class
        bool _foundGame;
        DateTime _lastFoundServer;
        GameInfo _gameInfo;

        const string _caption = "WC3 Proxy";
        const int _balloonTipTimeout = 1000;

        // TODO: Configurable command line arguments for war3?
        // window       Windowed mode
        // fullscreen   (Default)
        // gametype     ?
        // loadfile     Loads a map or replay
        // datadir      ?
        // classic      This will load in RoC mode even if you have TFT installed.
        // swtnl        Software Transform & Lighting
        // opengl
        // d3d          (Default)

        public MainForm(ServerInfo serverInfo)
        {
            _serverInfo = serverInfo;

            InitializeComponent();

            OnServerInfoChanged();
        }

        void ResetGameInfo()
        {
            icon.ShowBalloonTip(_balloonTipTimeout, _caption, "Lost game", ToolTipIcon.Info);

            gameNameValueLabel.Text = "(None found)";
            mapValueLabel.Text = "(N/A)";
            gamePortValueLabel.Text = "(N/A)";
            playerCountLValueLabel.Text = "(N/A)";

            _serverEP.Port = 0;

            _foundGame = false;
        }

        void DisplayGameInfo()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(DisplayGameInfo));
                return;
            }

            if (!_foundGame) icon.ShowBalloonTip(_balloonTipTimeout, _caption, "Found game: " + _gameInfo.Name, ToolTipIcon.Info);

            gameNameValueLabel.Text = _gameInfo.Name;
            mapValueLabel.Text = _gameInfo.Map;
            gamePortValueLabel.Text = _gameInfo.Port.ToString();
            playerCountLValueLabel.Text = String.Format("{0} / {1} / {2}", _gameInfo.CurrentPlayers, _gameInfo.PlayerSlots, _gameInfo.SlotCount);

            _serverEP.Port = _gameInfo.Port;
        }

        void ExecuteWC3(bool expansion)
        {
            string program = Configuration.GetExecutableFilename(expansion);

            if (program == null)
            {
                MessageBox.Show("Unable to locate Warcraft 3 executable");
                return;
            }

            try
            {
                Process.Start(program);
            }
            catch (Exception e)
            {
                string message = String.Format("Unable to launch WC3: {0}\n{1}", e.Message, program);
                MessageBox.Show(message, _caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        void FileExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void LaunchWarcraftMenuItem_Click(object sender, EventArgs e)
        {
            ExecuteWC3(_serverInfo.Expansion);
        }

        void MainForm_Shown(object sender, EventArgs e)
        {
            StartTcpProxy();
            StartBrowser();
        }

        void StartBrowser()
        {
            _browser = new Browser(_serverHost.AddressList[0], _listener.LocalEndPoint.Port, _serverInfo.Version, _serverInfo.Expansion);
            _browser.QuerySent += Browser_QuerySent;
            _browser.FoundServer += Browser_FoundServer;
            _browser.Run();
        }

        void Browser_FoundServer(GameInfo gameInfo)
        {
            _gameInfo = gameInfo;
            DisplayGameInfo();

            _foundGame = true;
            _lastFoundServer = DateTime.Now;
        }

        void Browser_QuerySent()
        {
            // We don't receive the "server cancelled" messages
            // because they are only ever broadcast to the host's LAN.
            if (_foundGame)
            {
                TimeSpan interval = DateTime.Now - _lastFoundServer;
                if (interval.TotalSeconds > 3)
                    OnLostGame();
            }
        }

        void OnLostGame()
        {
            if (_browser != null)
                _browser.SendGameCancelled(_gameInfo.GameId);

            if (_foundGame)
                Invoke(new Action(ResetGameInfo));
        }

        void StartTcpProxy()
        {
            _listener = new Listener(new GotConnectionDelegate(GotConnection));
            try
            {
                _listener.Run();
            }
            catch (SocketException ex)
            {
                MessageBox.Show("Unable to start listener\n" + ex.Message);
            }
        }

        void GotConnection(Socket clientSocket)
        {
            string message = String.Format("Got a connection from {0}", clientSocket.RemoteEndPoint.ToString());
            icon.ShowBalloonTip(_balloonTipTimeout, _caption, message, ToolTipIcon.Info);

            TcpProxy proxy = new TcpProxy(clientSocket, _serverEP);
            proxy.ProxyDisconnected += new ProxyDisconnectedHandler(ProxyDisconnected);
            lock (_proxies) _proxies.Add(proxy);

            proxy.Run();

            UpdateClientCount();
        }

        void UpdateClientCount()
        {
            if (InvokeRequired)
            {
                Invoke(new Action(UpdateClientCount));
                return;
            }

            clientCountValueLabel.Text = _proxies.Count.ToString();
        }

        void ProxyDisconnected(TcpProxy proxy)
        {
            icon.ShowBalloonTip(_balloonTipTimeout, _caption, "Client disconnected", ToolTipIcon.Info);

            lock (_proxies)
                if (_proxies.Contains(proxy)) _proxies.Remove(proxy);

            UpdateClientCount();
        }

        void StopTcpProxy()
        {
            _listener.Stop();
            foreach (TcpProxy p in _proxies)
                p.Stop();

            _proxies.Clear();
        }

        void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTcpProxy();
            if (_browser != null) _browser.Stop();
            if (_foundGame) _browser.SendGameCancelled(_gameInfo.GameId);
        }

        void Icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Focus();
        }

        void MainForm_Resize(object sender, EventArgs e)
        {
            ShowInTaskbar = (WindowState != FormWindowState.Minimized);
        }

        void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void ChangeServerMenuItem_Click(object sender, EventArgs e)
        {
            if (ServerInfoHelpers.ShowInfoDialog(_serverInfo))
            {
                OnLostGame();

                OnServerInfoChanged();
            }
        }

        void OnServerInfoChanged()
        {
            _serverHost = Dns.GetHostEntry(_serverInfo.Hostname);
            _serverEP = new IPEndPoint(_serverHost.AddressList[0], 0);

            string description;
            if (_serverHost.AddressList[0].ToString() == _serverHost.HostName)
                description = _serverHost.HostName;
            else
                description = String.Format("{0} ({1})", _serverHost.HostName, _serverHost.AddressList[0].ToString());

            serverAddressValueLabel.Text = description;

            if (_browser != null)
            {
                _browser.SetConfiguration(_serverHost.AddressList[0], _serverInfo.Version, _serverInfo.Expansion);
            }
        }

        void HelpAboutMenuItem_Click(object sender, EventArgs e)
        {
            using (var dlg = new AboutBox())
                dlg.ShowDialog();
        }
    }
}