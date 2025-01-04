namespace GluetunExtendarr.Core;

public interface IOvpnFileManager
{
    public string GetRemote();
    public void ReplaceRemote(string newRemote);
}

public class OvpnFileManager (IConfigFileProvider provider, IFileReader reader, IFileWriter writer) : IOvpnFileManager
{
    private const string SegmentSeparator = " ";
    private const string RemoteLinePrefix = "remote ";
    private const int RemoteHostIndex = 1;

    private readonly string filePath = provider.GetFile();
    private int RemoteLineIndex => this.Content.IndexOf(this.Content.Single(l => l.StartsWith(OvpnFileManager.RemoteLinePrefix)));
    private IList<string> Content => reader.Read(this.filePath).ToList();

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