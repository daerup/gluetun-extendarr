using GluetunExtendarr.Console;
using GluetunExtendarr.Core;
using Microsoft.Extensions.Configuration;

var settings = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build()
              .Get<Settings>()!;


var fileProvider = new ConfigConfigFileProvider(settings.ConfigName, settings.InputDir, settings.OutputDir);

var resolver = new HostnameResolver();
var manager = new OvpnFileManager(fileProvider, new FileReader(), new FileWriter(), new FileDuplicator());

string hostname = manager.GetRemote();
var ip = resolver.Resolve(hostname).ToString();
manager.ReplaceRemote(ip);
