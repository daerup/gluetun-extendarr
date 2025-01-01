namespace GluetunExtendarr.Core;

public class OvpnFileManager(IFileProvider provider, IFileReader reader, IFileWriter writer, IFileDuplicator duplicator)
{
    private const string SegmentSeparator = " ";
    private const string RemoteLinePrefix = "remote ";
    private const int RemoteHostIndex = 1;
    private int RemoteLineIndex => this.Content.IndexOf(this.Content.Single(l => l.StartsWith(OvpnFileManager.RemoteLinePrefix)));
    private readonly string filePath = provider.GetFile();
    public IList<string> Content => reader.Read(this.filePath).ToList();



    public string GetRemote() => this.GetLineSegments(this.GetRemoteLine())[OvpnFileManager.RemoteHostIndex];
    public void ReplaceRemote(string newRemote)
    {
        string oldLine = this.GetRemoteLine();
        string[] segments = this.GetLineSegments(oldLine);
        segments[OvpnFileManager.RemoteHostIndex] = newRemote;

        string newLine = string.Join(OvpnFileManager.SegmentSeparator, segments);

        string[] newContent = this.Content.Select(l => l == oldLine ? newLine : l).ToArray();
        writer.Write(this.filePath, newContent);
    }

    private string GetRemoteLine() => this.Content[this.RemoteLineIndex];
    private string[] GetLineSegments(string line) => line.Split(OvpnFileManager.SegmentSeparator);
}

public interface IFileProvider
{
    public string GetFile();
}
