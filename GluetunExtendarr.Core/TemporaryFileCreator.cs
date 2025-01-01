namespace GluetunExtendarr.Core;

public class TemporaryFileCreator : ITemporaryFileCreator
{
    public string CopyToTempDir(string originalFile)
    {
        string tempFile =  Path.GetTempFileName();
        File.Copy(originalFile, tempFile, true);
        return tempFile;
    }
}