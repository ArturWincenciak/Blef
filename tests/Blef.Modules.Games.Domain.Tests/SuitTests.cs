using Blef.Modules.Games.Domain.ValueObjects.Cards;

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
        Assert.True(Suit.Clubs.Equals(Suit.Clubs));
        Assert.True(Suit.Diamonds.Equals(Suit.Diamonds));
        Assert.True(Suit.Hearts.Equals(Suit.Hearts));
        Assert.True(Suit.Spades.Equals(Suit.Spades));
        Assert.False(Suit.Clubs.Equals(Suit.Diamonds));
        Assert.False(Suit.Diamonds.Equals(Suit.Hearts));
        Assert.False(Suit.Hearts.Equals(Suit.Spades));
        Assert.False(Suit.Spades.Equals(Suit.Clubs));
    }
}