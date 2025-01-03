﻿using System.Net;

namespace GluetunExtendarr.Core;

public interface IHostnameResolver
{
    public IPAddress Resolve(string hostname);
}

public class HostnameResolver : IHostnameResolver
{
    public IPAddress Resolve(string hostname)
    {
        var addresses = Dns.GetHostAddresses(hostname);
        var ipv4 = addresses.First(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork);
        return ipv4;
    }
}