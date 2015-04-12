using System;
using System.Net;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Foole.WC3Proxy
{
    sealed class ServerInfo
    {
        public string Hostname { get; set; }
        public bool Expansion { get; set; }
        public byte Version { get; set; }
    }

    static class ServerInfoHelpers
    {
        static readonly string _regPath = @"HKEY_CURRENT_USER\Software\Foole\WC3 Proxy";

        public static ServerInfo LoadServerInfo()
        {
            string servername = (string)Registry.GetValue(_regPath, "ServerName", null);
            if (servername == null)
                return null;

            return new ServerInfo
            {
                Hostname = servername,
                Expansion = ((int)Registry.GetValue(_regPath, "Expansion", 0)) != 0,
                Version = (byte)(int)Registry.GetValue(_regPath, "WC3Version", 0)
            };
        }

        public static bool ServerInfoIsValid(ServerInfo serverInfo)
        {
            if (serverInfo == null)
                return false;

            if (String.IsNullOrEmpty(serverInfo.Hostname))
                return false;

            try
            {
                Dns.GetHostEntry(serverInfo.Hostname);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool ShowInfoDialog(ServerInfo serverInfo)
        {
            using (var dlg = new ServerInfoDlg(serverInfo))
            {
                if (dlg.ShowDialog() == DialogResult.Cancel)
                    return false;
            }

            SaveServerInfo(serverInfo);

            return true;
        }

        public static void SaveServerInfo(ServerInfo serverInfo)
        {
            Registry.SetValue(_regPath, "ServerName", serverInfo.Hostname, RegistryValueKind.String);
            Registry.SetValue(_regPath, "Expansion", serverInfo.Expansion ? 1 : 0, RegistryValueKind.DWord);
            Registry.SetValue(_regPath, "WC3Version", serverInfo.Version, RegistryValueKind.DWord);
        }
    }
}
