using System;
using System.Net;

namespace Foole.WC3Proxy.Net
{
    public static class Utilities
    {
        public static IPAddress ParseOrResolveIPAddress(string addressText)
        {
            if (String.IsNullOrEmpty(addressText))
                return null;

            IPAddress address;
            if (IPAddress.TryParse(addressText, out address))
                return address;

            try
            {
                var serverHostEntry = Dns.GetHostEntry(addressText);
                if (serverHostEntry == null)
                    return null;

                return serverHostEntry.AddressList[0];
            }
            catch // SocketException : No such host is known.
            {
                return null;
            }
        }
    }
}
