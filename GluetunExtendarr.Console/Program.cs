// See https://aka.ms/new-console-template for more information

using GluetunExtendarr.Core;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");

var configuration = new ConfigurationBuilder().AddEnvironmentVariables().Build();


var inputDir1 = configuration["INPUT_DIR"];
var outputDir2 = configuration["OUTPUT_DIR"];
var configName = configuration["CONFIG_NAME"];

var inputDir = "";
var outputDir = "";

var newFile = "C:\\Users\\daerup\\Documents\\repos\\openvpn\\dev\\configs\\madrid.ovpn";

while (true)
{
    Console.WriteLine($"Inputdir: {inputDir1}");
    Thread.Sleep(TimeSpan.FromSeconds(10));
}


var resolver = new HostnameResolver();
var manager = new OvpnFileManager(newFile, new FileReader(), new FileWriter());


var hostname = manager.GetRemote();
var ip = resolver.Resolve(hostname);
//manager.ReplaceRemote(ip.ToString());
