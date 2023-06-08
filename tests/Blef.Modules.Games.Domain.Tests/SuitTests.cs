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
        Assert.Equal(clubs, Suit.Create("clubs"));
        Assert.Equal(diamonds, Suit.Create("diamonds"));
        Assert.Equal(hearts, Suit.Create("hearts"));
        Assert.Equal(spades, Suit.Create("spades"));
    }

    [Fact]
    public void SuitEqualsTests()
    {
        Assert.False(Suit.Clubs.Equals(Suit.Diamonds));
        Assert.False(Suit.Diamonds.Equals(Suit.Hearts));
        Assert.False(Suit.Hearts.Equals(Suit.Spades));
        Assert.False(Suit.Spades.Equals(Suit.Clubs));
    }

    [Fact]
    public void RankTests()
    {
        Assert.True(Suit.Clubs.GetRank() < Suit.Diamonds.GetRank());
        Assert.True(Suit.Diamonds.GetRank() < Suit.Hearts.GetRank());
        Assert.True(Suit.Hearts.GetRank() < Suit.Spades.GetRank());
        Assert.True(Suit.Spades.GetRank() > Suit.Clubs.GetRank());
    }
}
