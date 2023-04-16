using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class GameIdTests
{
    [Fact]
    public void CreateGameIdTest()
    {
        var guid = Guid.Parse("68FE550A-707A-4864-9EE7-5580FF626A0D");
        var exception = Record.Exception(() => new GameId(guid));
        Assert.Null(exception);
    }

    [Fact]
    public void GameIdCannotContainEmptyGuid() =>
        Assert.Throws<ArgumentException>(() => new GameId(Guid.Empty));

    [Fact]
    public void TwoInstancesWithTheSameGuidAreEqualsTest()
    {
        // arrange
        var guid = Guid.Parse("FB2CD957-F951-4BB1-B4C1-D575E41AF924");
        var first = new GameId(guid);
        var second = new GameId(guid);

        // act
        var actual = first == second;

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void TwoInstancesWithDifferentGuidAreNotEqualTest()
    {
        // arrange
        var first = new GameId(Guid.Parse("0716862C-AF62-4D3B-8415-4F005A49142D"));
        var second = new GameId(Guid.Parse("9327DD0F-64C4-4DDF-BCE8-3A48C49D78EF"));

        // act
        var actual = first != second;

        // assert
        Assert.True(actual);
    }
}