using GluetunExtendarr.Core;
using Microsoft.Extensions.Options;

namespace GluetunExtendarr.App;

internal static class Extensions
{
    public static void AddExtendarrServices(this IServiceCollection services)
    {
        services.AddSingleton<ExtendarrRunner>();
        services.AddSingleton<ICompletable>(x => x.GetRequiredService<ExtendarrRunner>());

        services.AddTransient<IFileReader, FileReader>();
        services.AddTransient<IFileWriter, FileWriter>();
        services.AddTransient<IFileWriter, FileWriter>();
        services.AddTransient<IOvpnFileManager, OvpnFileManager>();
        services.AddTransient<IHostnameResolver, HostnameResolver>();
        services.AddSingleton<IConfigFileProvider>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<Settings>>().Value;
            return new ConfigFileProvider(settings.ConfigName, settings.InputDir, settings.OutputDir);
        });
    }

    public static void LogMinLevel(this WebApplication app)
    {
        var minimumLogLevel = app.Configuration.GetValue<string>("Serilog:MinimumLevel:Default");
        app.Logger.LogInformation("Current minimum logging level: {Level}", minimumLogLevel);
    }

    public static void RegisterRunableOnStartUp<T>(this WebApplication app) where T : IRunable
    {
        app.Lifetime.ApplicationStarted.Register(() =>
        {
            var runner = app.Services.GetRequiredService<T>();
            runner.Run();
        });
    }
}