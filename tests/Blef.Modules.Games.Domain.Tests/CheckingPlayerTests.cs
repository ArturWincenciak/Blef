using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class CheckingPlayerTests
{
    [Fact]
    public void CreateCheckingPlayerTest()
    {
        // arrange
        var guid = Guid.Parse("F51D3D3C-BF45-4FE8-AA41-D07BF43D9A14");

        // act
        var actual = new CheckingPlayer(guid);

        // assert
        Assert.Equal(guid, actual.PlayerId);
        Assert.True(new CheckingPlayer(guid) == actual);
    }

    [Fact]
    public void CanCreateCheckingPlayerWithEmptyGuidTest()
    {
        // arrange
        var emptyGuid = Guid.Empty;

        // act
        var actual = new CheckingPlayer(emptyGuid);

        // assert
        Assert.Equal(emptyGuid, actual.PlayerId);
        Assert.True(new CheckingPlayer(emptyGuid) == actual);
    }
}