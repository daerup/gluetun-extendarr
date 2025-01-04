using GluetunExtendarr.App;
using Serilog;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json").AddEnvironmentVariables();
builder.Services.Configure<Settings>(builder.Configuration);
builder.Services.AddSerilog(c => c.ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().WriteTo.Console());

builder.Services.AddExtendarrServices();

builder.Services.AddHealthChecks().AddCheck<HasCompletedHealthCheck>($"{nameof(ExtendarrRunner)}{nameof(ExtendarrRunner.HasCompleted)}");

var app = builder.Build();
app.MapHealthChecks("/healthz");

app.LogMinLevel();
app.RegisterRunableOnStartUp<ExtendarrRunner>();

app.Run();