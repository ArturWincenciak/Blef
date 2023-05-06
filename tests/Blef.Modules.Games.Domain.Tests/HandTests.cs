using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class HandTests
{
    [Fact]
    public void CreateHandTest()
    {
        // arrange
        var cards = new Card[]
        {
            new(FaceCard.Ace, Suit.Clubs),
            new(FaceCard.King, Suit.Hearts),
            new(FaceCard.Queen, Suit.Diamonds)
        };

        // act
        var actual = new Hand(cards);

        // assert
        Assert.Equal(cards, actual.Cards);
    }

    [Fact]
    public void CannotCreateHandWithNullCardsCollectionTest() =>
        Assert.Throws<ArgumentNullException>(() => new Hand(null));

    [Fact]
    public void CannotCreateHandWithoutAnyCardTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new Hand(Array.Empty<Card>()));

    [Fact]
    public void CannotCreateHandWithMoreThanFiveCardsTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Hand(new Card[]
            {
                new(FaceCard.Ace, Suit.Clubs),
                new(FaceCard.King, Suit.Clubs),
                new(FaceCard.Queen, Suit.Clubs),
                new(FaceCard.Jack, Suit.Clubs),
                new(FaceCard.Ten, Suit.Clubs),
                new(FaceCard.Nine, Suit.Clubs)
            }));

    [Fact]
    public void CannotCreateHandWithNotUniqueCardsTest() =>
        Assert.Throws<ArgumentException>(() => new Hand(new Card[]
        {
            new(FaceCard.Ace, Suit.Diamonds),
            new(FaceCard.Ace, Suit.Diamonds)
        }));
}