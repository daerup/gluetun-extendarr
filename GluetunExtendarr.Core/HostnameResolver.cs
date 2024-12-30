using System.Net;

namespace GluetunExtendarr.Core;

public class HostnameResolver{
    public IPAddress Resolve(string hostname)
    {
        var addresses = Dns.GetHostAddresses(hostname);
        IPAddress ipv4 = addresses.First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        return ipv4;
    }
}