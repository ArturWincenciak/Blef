using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Tests;

public class TableTests
{
    [Fact]
    public void CreateTableTest()
    {
        // arrange
        var hands = new Hand[]
        {
            new(new Card[] { new(FaceCard.Ace, Suit.Clubs) }),
            new(new Card[] { new(FaceCard.King, Suit.Hearts) }),
            new(new Card[] { new(FaceCard.King, Suit.Clubs) })
        };

        // act
        var actual = new Table(hands);

        // assert
        Assert.Equal(expected: 1, actual.Count(FaceCard.Ace));
        Assert.Equal(expected: 2, actual.Count(FaceCard.King));
        Assert.True(actual.Contains(FaceCard.Ace));
        Assert.True(actual.Contains(FaceCard.King));
        Assert.False(actual.Contains(FaceCard.Queen));
        Assert.False(actual.Contains(FaceCard.Jack));
        Assert.False(actual.Contains(FaceCard.Ten));
        Assert.False(actual.Contains(FaceCard.Nine));
    }
}