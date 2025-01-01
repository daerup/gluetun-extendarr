namespace GluetunExtendarr.Core;

public interface IFileDuplicator
{
    public void Copy(string source, string destination);
}

public class FileDuplicator : IFileDuplicator
{
    public void Copy(string source, string destination) => File.Copy(source, destination, true);
}