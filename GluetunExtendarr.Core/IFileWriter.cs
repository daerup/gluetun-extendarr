namespace GluetunExtendarr.Core;

public interface IFileWriter
{
    void Write(string path, string[] lines);
}