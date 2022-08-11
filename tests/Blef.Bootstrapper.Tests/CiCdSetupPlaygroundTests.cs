namespace Blef.Bootstrapper.Tests;

public class ContinuesIntegrationPlaygroundTests
{
    [Fact]
    public void AlwaysGreen()
    {
        Assert.True(true);
    }

    [Fact]
    public void AlwaysRed()
    {
        Assert.True(false);
    }
}