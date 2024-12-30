using System.Reflection;

namespace GluetunExtendarr.Core.Test;

internal class TestHelper
{
    private const string SampleFileName = "sample.ovpn";
    internal IEnumerable<string> GetSampleOvpnFile()
    {
        var assembly = Assembly.GetExecutingAssembly();
        string resourceName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(TestHelper.SampleFileName));

        using Stream stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        while (!reader.EndOfStream)
        {
            yield return reader.ReadLine()!;
        }
    }
}