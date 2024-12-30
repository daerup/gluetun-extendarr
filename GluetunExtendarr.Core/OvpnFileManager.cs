using System.IO;

namespace GluetunExtendarr.Core;

public class OvpnFileManager(string filePath, IFileReader reader, IFileWriter writer) : IOvpnFileManager
{
    private const string SegmentSeparator = " ";
    private const int RemoteLineIndex = 2;
    private const int RemoteHostIndex = 1;
    public IList<string> Content => reader.Read(filePath).ToList();

    public string GetRemote() => this.GetRemoteSegments()[OvpnFileManager.RemoteHostIndex];
    public void ReplaceRemote(string newRemote)
    {
        string[] segments = this.GetRemoteSegments();
        segments[OvpnFileManager.RemoteHostIndex] = newRemote;
        string[] newContent = this.ReplaceRemoteLine(segments);
        writer.Write(filePath, newContent);
    }

    private string[] GetRemoteSegments() => this.Content[OvpnFileManager.RemoteLineIndex].Split(OvpnFileManager.SegmentSeparator);
    private string[] ReplaceRemoteLine(string[] newSegments) => this.Content.Select((line, index) => index == OvpnFileManager.RemoteLineIndex ? string.Join(OvpnFileManager.SegmentSeparator, newSegments) : line).ToArray();
}