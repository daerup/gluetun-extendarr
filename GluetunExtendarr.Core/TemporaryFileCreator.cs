using System.Net;

namespace GluetunExtendarr.Core;

public class TemporaryFileCreator : ITemporaryFileCreator
{
    public string CopyToTempDir(string originalFile)
    {
        string tempFile =  Path.GetTempFileName();
        File.Copy(originalFile, tempFile, true);
        return tempFile;
    }
}

public class HostnameResolver{
    public IPAddress Resolve(string hostname)
    {
        var hostAddresses = Dns.GetHostAddresses(hostname);
        IPAddress ipv4 = hostAddresses.First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        return ipv4;
    }
}