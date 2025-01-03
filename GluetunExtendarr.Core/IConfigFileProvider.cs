﻿namespace GluetunExtendarr.Core;

public interface IConfigFileProvider
{
    public string GetFile();
}

public class ConfigFileProvider(string fileName, string sourceDir, string destinationDir) : IConfigFileProvider
{
    public string GetFile()
    {
        string source = Path.GetFullPath(Path.Combine(sourceDir, fileName));
        string destination = Path.GetFullPath(Path.Combine(destinationDir, fileName));
        File.Copy(source, destination, true);
        return destination;
    }
}