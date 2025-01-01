using GluetunExtendarr.Console;
using GluetunExtendarr.Core;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .AddEnvironmentVariables()
              .Build()
              .Get<Config>()!;

var fileDuplicator = new TemporaryFileCreator();

string originalConfigFile = Path.Combine(config.InputDir, config.ConfigName);
string duplicatedConfigFile = fileDuplicator.CopyToTempDir(originalConfigFile);

var resolver = new HostnameResolver();
var manager = new OvpnFileManager(duplicatedConfigFile, new FileReader(), new FileWriter());

string hostname = manager.GetRemote();
var ip = resolver.Resolve(hostname).ToString();
//manager.ReplaceRemote(ip);


