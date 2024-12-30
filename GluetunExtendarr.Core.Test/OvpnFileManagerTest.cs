using FakeItEasy;
using FluentAssertions;

namespace GluetunExtendarr.Core.Test;

public class OvpnFileManagerTest
{
    private readonly TestHelper helper = new TestHelper();
    private readonly string fakeFilePath;
    private readonly IFileReader fileReader;
    private readonly IFileWriter fileWriter;

    public OvpnFileManagerTest()
    {
        this.fakeFilePath = "somePath/ovpn.ovpn";
        this.fileReader = A.Fake<IFileReader>();
        this.fileWriter = A.Fake<IFileWriter>();
        A.CallTo(() => this.fileReader.Read(this.fakeFilePath)).Returns(this.helper.GetSampleOvpnFile());
    }

    [Fact]
    public void CanGetRemote()
    {
        // Assert 
        var testee = new OvpnFileManager(this.fakeFilePath, this.fileReader, this.fileWriter);

        // Act
        string remote = testee.GetRemote();

        // Assert
        remote.Should().Be("example.com");
    }

    [Fact]
    public void CanReplaceRemote()
    {
        // Arrange
        var testee = new OvpnFileManager(this.fakeFilePath, this.fileReader, this.fileWriter);
        const string newRemote = "someIpAddress";
        const string expectedFullRemote = $"remote {newRemote} 1194";

        // Act
        testee.ReplaceRemote(newRemote);

        // Assert
        A.CallTo(() => this.fileReader.Read(this.fakeFilePath)).MustHaveHappened();
        A.CallTo(() => this.fileWriter.Write(this.fakeFilePath, A<string[]>._)).MustHaveHappenedOnceExactly();
        A.CallTo(() => this.fileWriter.Write(this.fakeFilePath, A<string[]>.That.Contains(expectedFullRemote))).MustHaveHappened();
    }
}