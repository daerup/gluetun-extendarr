namespace GluetunExtendarr.Core;

public class TemporaryFileCreator : ITemporaryFileCreator
{
    public string CopyToTempDir(string originalFile)
    {
        var tempFile =  Path.GetTempFileName();
        File.Copy(originalFile, tempFile, true);
        return tempFile;
    }
}