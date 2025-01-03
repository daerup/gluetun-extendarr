using GluetunExtendarr.App;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();
builder.Services.Configure<Settings>(builder.Configuration);
builder.Services.AddHealthChecks().AddCheck("Docker Check", Endpoints.HealthCheckAction);
builder.Services.AddSerilog(c => c.ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().WriteTo.Console());

var app = builder.Build();
app.MapHealthChecks("/healthz");

var minimumLogLevel = app.Configuration.GetValue<string>("Serilog:MinimumLevel:Default");
app.Logger.LogInformation("Current minimum logging level: {Level}", minimumLogLevel);

app.Lifetime.ApplicationStarted.Register(() =>
{
    var settings = app.Services.GetRequiredService<IOptions<Settings>>();
    var logger = app.Services.GetRequiredService<ILogger<Endpoints>>();
    Endpoints.RunAction(settings, logger);
});

app.Run();
