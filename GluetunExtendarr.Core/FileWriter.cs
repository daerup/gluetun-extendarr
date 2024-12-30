using System.Text;

namespace GluetunExtendarr.Core;

public class FileWriter : IFileWriter
{
    public void Write(string path, string[] lines) => File.WriteAllLines(path, lines, Encoding.UTF8);
}