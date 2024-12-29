using FluentAssertions;
using GluetunExtendarr.Core;

namespace GluetunExtendarr.Test;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // Assert 
        const string someInput = "dummy";
        var testee = new Class1();

        // Act
        string result = testee.SomeMethod(someInput);

        // Assert
        result.Should().Be("DUMMY");
    }
}