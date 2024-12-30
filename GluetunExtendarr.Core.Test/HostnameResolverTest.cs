using System.Net;
using System.Runtime.InteropServices;
using FluentAssertions;

namespace GluetunExtendarr.Core.Test;

public class HostnameResolverTest
{
    [Theory]
    [InlineData("localhost", "127.0.0.1")]
    [InlineData("dns.google.com", "8.8.4.4", "8.8.8.8")]
    public void ResolveLocalhost(string hostname, params string[] ips)
    {
        // Arrange
        var testee = new HostnameResolver();

        // Act
        IPAddress result = testee.Resolve(hostname);

        // Assert
        result.ToString().Should().BeOneOf(ips);
    }
}