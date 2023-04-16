using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class PlayerNickTests
{
    [Fact]
    public void CreatePlayerNickTest()
    {
        // arrange
        var nick = "Kent";

        // act
        var playerNick = new PlayerNick(nick);

        // assert
        Assert.Equal(nick, playerNick.Nick);
    }

    [Fact]
    public void NickCannotBeNullTest() =>
        Assert.Throws<ArgumentException>(() => new PlayerNick(null));

    [Fact]
    public void NickCannotBeEmptyTest() =>
        Assert.Throws<ArgumentException>(() => new PlayerNick(""));

    [Fact]
    public void NickCannotBeWhiteSpacesTest() =>
        Assert.Throws<ArgumentException>(() => new PlayerNick("  "));

    [Fact]
    public void TwoInstancesOfPlayerNickWithTheSameNickAreEqualsTest()
    {
        // arrange
        var nick = "Kent";
        var first = new PlayerNick(nick);
        var second = new PlayerNick(nick);

        // act
        var actual = first == second;

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void TwoInstancesOfPlayerNickWithDifferentNickAreNotEqualTest()
    {
        // arrange
        var first = new PlayerNick("Kent");
        var second = new PlayerNick("Graham");

        // act
        var actual = first != second;

        // assert
        Assert.True(actual);
    }
}