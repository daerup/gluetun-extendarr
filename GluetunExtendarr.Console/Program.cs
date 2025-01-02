using GluetunExtendarr.Console;
using GluetunExtendarr.Core;
using Microsoft.Extensions.Configuration;

var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build().Get<Settings>()!;
var fileProvider = new ConfigFileProvider(settings.ConfigName, settings.InputDir, settings.OutputDir);

var resolver = new HostnameResolver();
var manager = new OvpnFileManager(fileProvider, new FileReader(), new FileWriter());

string hostname = manager.GetRemote();

Console.WriteLine($"Resolving hostname {hostname}...");
var ip = resolver.Resolve(hostname).ToString();
Console.WriteLine($"Resolved hostname {hostname} to IP {ip}");
manager.ReplaceRemote(ip);
Console.WriteLine("Replaced hostname with IP in ovpn file");
Console.WriteLine("Done");
