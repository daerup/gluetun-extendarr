namespace GluetunExtendarr.Core;

public interface IFileReader
{
    IEnumerable<string> Read(string path);
}