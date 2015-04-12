using Microsoft.Win32;

namespace Foole.WC3Proxy.Warcraft3
{
    static class Configuration
    {
        public static string GetExecutableFilename(bool expansion)
        {
            string programKey = expansion ? "ProgramX" : "Program";

            return (string)Registry.GetValue(@"HKEY_CURRENT_USER\Software\Blizzard Entertainment\Warcraft III", programKey, null);
        }
    }
}
