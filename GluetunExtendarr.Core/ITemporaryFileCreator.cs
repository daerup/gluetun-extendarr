namespace GluetunExtendarr.Core;

public interface ITemporaryFileCreator
{
    string CopyToTempDir(string originalFile);
}