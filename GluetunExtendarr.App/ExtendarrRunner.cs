using GluetunExtendarr.Core;

namespace GluetunExtendarr.App;

public interface ICompletable
{
    public bool HasCompleted { get; }
}

public interface IRunable : ICompletable
{
    public void Run();
}

internal class ExtendarrRunner(ILogger<ExtendarrRunner> logger, IOvpnFileManager manager, IHostnameResolver resolver) : IRunable
{
    public bool HasCompleted { get; private set; }

    public void Run()
    {
        string hostname = manager.GetRemote();

        logger.LogInformation("Resolving hostname {Hostname}...", hostname);
        var ip = resolver.Resolve(hostname).ToString();
        logger.LogInformation("Resolved hostname {Hostname} to IP {IP}", hostname, ip);
        manager.ReplaceRemote(ip);
        logger.LogInformation("Replaced hostname with IP in ovpn file");
        logger.LogInformation("Done");
        HasCompleted = true;
    }
}