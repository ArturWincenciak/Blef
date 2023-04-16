using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class CheckingPlayerTests
{
    [Fact]
    public void CreateCheckingPlayerTest()
    {
        var guid = Guid.Parse("F51D3D3C-BF45-4FE8-AA41-D07BF43D9A14");
        var exception = Record.Exception(() => new CheckingPlayer(guid));
        Assert.Null(exception);
    }

    [Fact]
    public void CanCreateCheckingPlayerWithEmptyGuidTest()
    {
        var emptyGuid = Guid.Empty;
        var exception = Record.Exception(() => new CheckingPlayer(emptyGuid));
        Assert.Null(exception);
    }
}