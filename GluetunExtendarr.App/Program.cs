using GluetunExtendarr.App;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();
builder.Services.Configure<Settings>(builder.Configuration);
builder.Services.AddHealthChecks().AddCheck("Docker Check", Endpoints.HealthCheckAction);

var app = builder.Build();

app.MapPost("/run", Endpoints.RunAction);
app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();


app.Run();
