using GluetunExtendarr.Core;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace GluetunExtendarr.App;

internal abstract class Endpoints
{
    private static bool hasRun;

    internal static void RunAction(IOptions<Settings> settings, ILogger<Endpoints> logger)
    {
        var fileProvider = new ConfigFileProvider(settings.Value.ConfigName, settings.Value.InputDir, settings.Value.OutputDir);
        var resolver = new HostnameResolver();
        var manager = new OvpnFileManager(fileProvider, new FileReader(), new FileWriter());

        string hostname = manager.GetRemote();

        logger.LogInformation("Resolving hostname {Hostname}...", hostname);
        var ip = resolver.Resolve(hostname).ToString();
        logger.LogInformation("Resolved hostname {Hostname} to IP {IP}", hostname, ip);
        manager.ReplaceRemote(ip);
        logger.LogInformation("Replaced hostname with IP in ovpn file");
        logger.LogInformation("Done");
        hasRun = true;
    }

    public static HealthCheckResult HealthCheckAction() => hasRun ? HealthCheckResult.Healthy("Config file has been created") : HealthCheckResult.Unhealthy("GluetunExtendarr has not run yet");
}