using System.Text;

namespace GluetunExtendarr.Core;

public class FileReader : IFileReader
{
    public IEnumerable<string> Read(string path)
    {
        using var streamReader = new StreamReader(path, Encoding.UTF8);

        while (!streamReader.EndOfStream)
        {
            yield return streamReader.ReadLine() ?? throw new InvalidOperationException();
        }
    }
}