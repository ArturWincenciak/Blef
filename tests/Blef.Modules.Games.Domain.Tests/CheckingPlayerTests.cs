using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class CheckingPlayerTests
{
    [Fact]
    public void CreateCheckingPlayerTest()
    {
        var exception = Record.Exception(() =>
        {
            // arrange
            var guid = Guid.Parse("F51D3D3C-BF45-4FE8-AA41-D07BF43D9A14");

            // act
            var actual = new CheckingPlayer(guid);

            // assert
            Assert.Equal(expected: guid, actual: actual.PlayerId);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void CanCreateCheckingPlayerWithEmptyGuidTest()
    {
        var exception = Record.Exception(() =>
        {
            // arrange
            var emptyGuid = Guid.Empty;

            // act
            var actual = new CheckingPlayer(emptyGuid);

            // assert
            Assert.Equal(expected: emptyGuid, actual: actual.PlayerId);
        });
        Assert.Null(exception);
    }
}