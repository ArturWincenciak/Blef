namespace Blef.Bootstrapper.Tests;

public class ContinuesIntegrationPlaygroundTests
{
    [Fact]
    public void AlwaysGreen()
    {
        Assert.True(true);
    }

    [Fact(Skip = "Skip to test CI/CD.")]
    public void AlwaysRed()
    {
        Assert.True(false);
    }
}

// comment to create diff that can be creat new PR
