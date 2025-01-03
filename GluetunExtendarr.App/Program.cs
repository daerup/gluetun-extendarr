using GluetunExtendarr.App;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Logging.AddConsole();
builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();
builder.Services.Configure<Settings>(builder.Configuration);
builder.Services.AddHealthChecks().AddCheck("Docker Check", Endpoints.HealthCheckAction);
var app = builder.Build();

app.MapPost("/run", Endpoints.RunAction);
app.MapHealthChecks("/healthz");

app.Run();
