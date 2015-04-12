using System.Windows.Forms;

namespace Foole.WC3Proxy
{
    static class Program
    {
        static void Main(string[] args)
        {
            var serverInfo = ServerInfoHelpers.LoadServerInfo();

            if (!ServerInfoHelpers.ServerInfoIsValid(serverInfo))
            {
                if (serverInfo == null)
                    serverInfo = new ServerInfo();

                var result = ServerInfoHelpers.ShowInfoDialog(serverInfo);
                if (!result)
                    return;
            }

            var mainForm = new MainForm(serverInfo);

            Application.Run(mainForm);
        }
    }
}
