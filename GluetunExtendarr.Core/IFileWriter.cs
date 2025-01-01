using System.Text;

namespace GluetunExtendarr.Core;

public interface IFileWriter
{
    public void Write(string path, string[] lines);
}

public class FileWriter : IFileWriter
{
    public void Write(string path, string[] lines) => File.WriteAllLines(path, lines, Encoding.UTF8);
}