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
using System.Windows.Forms;
using Foole.WC3Proxy.Net;

namespace Foole.WC3Proxy
{
    struct WC3Version
    {
        public readonly byte Id;
        public readonly string Description;

        public WC3Version(byte id, string description)
        {
            Id = id;
            Description = description;
        }

        public override string ToString()
        {
            return Description;
        }
    }

    sealed partial class ServerInfoDlg : Form
    {
        ServerInfo _serverInfo;

        public ServerInfoDlg(ServerInfo serverInfo)
        {
            _serverInfo = serverInfo;

            InitializeComponent();

            versionComboBox.Items.Add(new WC3Version(0x1c, "1.28"));
            versionComboBox.Items.Add(new WC3Version(0x1b, "1.27"));
            versionComboBox.Items.Add(new WC3Version(0x1a, "1.26"));
            versionComboBox.Items.Add(new WC3Version(0x19, "1.25"));
            versionComboBox.Items.Add(new WC3Version(0x18, "1.24"));
            versionComboBox.Items.Add(new WC3Version(0x17, "1.23"));
            versionComboBox.Items.Add(new WC3Version(0x16, "1.22"));
            versionComboBox.Items.Add(new WC3Version(0x15, "1.21"));

            serverAddressTextBox.Text = serverInfo.Hostname;

            expansionCheckBox.Checked = serverInfo.Expansion;

            foreach (WC3Version vers in versionComboBox.Items)
            {
                if (vers.Id == serverInfo.Version)
                {
                    versionComboBox.SelectedItem = vers;
                    break;
                }
            }
        }

        void OkButton_Click(object sender, EventArgs e)
        {
            if (!ValidateServerAddress(serverAddressTextBox.Text))
            {
                serverAddressTextBox.Focus();
                return;
            }

            _serverInfo.Hostname = serverAddressTextBox.Text;
            _serverInfo.Expansion = expansionCheckBox.Checked;
            var version = (WC3Version)versionComboBox.SelectedItem;
            _serverInfo.Version = version.Id;

            DialogResult = DialogResult.OK;
            Hide();
        }

        // An address is valid if it can be parsed or resolved to an IPAddress
        bool ValidateServerAddress(string address)
        {
            if (String.IsNullOrEmpty(address))
            {
                MessageBox.Show("Please enter a server address", "WC3 Proxy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (Utilities.ParseOrResolveIPAddress(address) == null)
            {
                MessageBox.Show("Server address is invalid", "WC3 Proxy", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        void CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Hide();
        }
    }
}