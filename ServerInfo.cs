using System.Windows.Forms;
using Foole.WC3Proxy.Net;
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
        static readonly string _regPathFull = @"HKEY_CURRENT_USER\Software\Foole\WC3 Proxy";
        static readonly string _regPathSubKey = @"Software\Foole\WC3 Proxy";
        static readonly string _regPathTopLevelSubKey = @"Software\Foole";

        public static ServerInfo LoadServerInfo()
        {
            string servername = (string)Registry.GetValue(_regPathFull, "ServerName", null);
            if (servername == null)
                return null;

            return new ServerInfo
            {
                Hostname = servername,
                Expansion = ((int)Registry.GetValue(_regPathFull, "Expansion", 0)) != 0,
                Version = (byte)(int)Registry.GetValue(_regPathFull, "WC3Version", 0)
            };
        }

        public static bool ServerInfoIsValid(ServerInfo serverInfo)
        {
            if (serverInfo == null)
                return false;

            if (Utilities.ParseOrResolveIPAddress(serverInfo.Hostname) == null)
                return false;

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
            Registry.SetValue(_regPathFull, "ServerName", serverInfo.Hostname, RegistryValueKind.String);
            Registry.SetValue(_regPathFull, "Expansion", serverInfo.Expansion ? 1 : 0, RegistryValueKind.DWord);
            Registry.SetValue(_regPathFull, "WC3Version", serverInfo.Version, RegistryValueKind.DWord);
        }

        public static void DeleteFromRegistry()
        {
            Registry.CurrentUser.DeleteSubKey(_regPathSubKey, false);
            try
            {
                Registry.CurrentUser.DeleteSubKey(_regPathTopLevelSubKey);
            }
            catch { } // InvalidOperationException: Registry key has subkeys and recursive removes are not supported by this method.
        }
    }
}
