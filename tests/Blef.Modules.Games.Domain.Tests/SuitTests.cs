using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class SuitTests
{
    [Fact]
    public void CreateSuitTest()
    {
        // act
        var clubs = Suit.Clubs;
        var diamonds = Suit.Diamonds;
        var hearts = Suit.Hearts;
        var spades = Suit.Spades;

        // assert
        Assert.NotNull(clubs);
        Assert.NotNull(diamonds);
        Assert.NotNull(hearts);
        Assert.NotNull(spades);
    }

    [Fact]
    public void SuitEqualsTests()
    {
        Assert.False(Suit.Clubs.Equals(Suit.Diamonds));
        Assert.False(Suit.Diamonds.Equals(Suit.Hearts));
        Assert.False(Suit.Hearts.Equals(Suit.Spades));
        Assert.False(Suit.Spades.Equals(Suit.Clubs));
    }
}