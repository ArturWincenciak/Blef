using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class TableTests
{
    [Fact]
    public void CreateTableTest()
    {
        // arrange
        var hands = new Hand[]
        {
            new(new Card[] {new(FaceCard.Ace, Suit.Clubs)}),
            new(new Card[] {new(FaceCard.King, Suit.Hearts)}),
            new(new Card[] {new(FaceCard.King, Suit.Clubs)})
        };

        // act
        var actual = new Table(hands);

        // assert
        Assert.Equal(expected: 1, actual: actual.Count(FaceCard.Ace));
        Assert.Equal(expected: 2, actual: actual.Count(FaceCard.King));
        Assert.Equal(expected: 1, actual: actual.Count(Suit.Hearts));
        Assert.Equal(expected: 2, actual: actual.Count(Suit.Clubs));
        Assert.True(actual.Contains(FaceCard.Ace));
        Assert.True(actual.Contains(FaceCard.King));
        Assert.False(actual.Contains(FaceCard.Queen));
        Assert.False(actual.Contains(FaceCard.Jack));
        Assert.False(actual.Contains(FaceCard.Ten));
        Assert.False(actual.Contains(FaceCard.Nine));
    }

    [Fact]
    public void CreateMinTableTest()
    {
        // arrange
        var hands = new Hand[]
        {
            new(new Card[] {new(FaceCard.Nine, Suit.Clubs)}),
            new(new Card[] {new(FaceCard.Nine, Suit.Diamonds)})
        };

        // act
        var actual = new Table(hands);

        // assert
        Assert.Equal(expected: 2, actual: actual.Count(FaceCard.Nine));
        Assert.Equal(expected: 1, actual: actual.Count(Suit.Clubs));
        Assert.Equal(expected: 1, actual: actual.Count(Suit.Diamonds));
        Assert.True(actual.Contains(FaceCard.Nine));
        Assert.False(actual.Contains(FaceCard.Ten));
        Assert.False(actual.Contains(FaceCard.Jack));
        Assert.False(actual.Contains(FaceCard.Queen));
        Assert.False(actual.Contains(FaceCard.King));
        Assert.False(actual.Contains(FaceCard.Ace));
    }

    [Fact]
    public void CreateMaxTableTest()
    {
        // arrange
        var hands = new Hand[]
        {
            new(new Card[]
            {
                new(FaceCard.Ace, Suit.Clubs),
                new(FaceCard.Ace, Suit.Diamonds),
                new(FaceCard.Ace, Suit.Hearts),
                new(FaceCard.Ace, Suit.Spades),
                new(FaceCard.King, Suit.Clubs)
            }),
            new(new Card[]
            {
                new(FaceCard.King, Suit.Diamonds),
                new(FaceCard.King, Suit.Hearts),
                new(FaceCard.King, Suit.Spades),
                new(FaceCard.Queen, Suit.Clubs),
                new(FaceCard.Queen, Suit.Diamonds)
            }),
            new(new Card[]
            {
                new(FaceCard.Queen, Suit.Hearts),
                new(FaceCard.Queen, Suit.Spades),
                new(FaceCard.Jack, Suit.Clubs),
                new(FaceCard.Jack, Suit.Diamonds),
                new(FaceCard.Jack, Suit.Hearts)
            }),
            new(new Card[]
            {
                new(FaceCard.Jack, Suit.Spades),
                new(FaceCard.Ten, Suit.Clubs),
                new(FaceCard.Ten, Suit.Diamonds),
                new(FaceCard.Ten, Suit.Hearts),
                new(FaceCard.Ten, Suit.Spades)
            })
        };

        // act
        var actual = new Table(hands);

        // assert
        Assert.Equal(expected: 4, actual: actual.Count(FaceCard.Ace));
        Assert.Equal(expected: 4, actual: actual.Count(FaceCard.King));
        Assert.Equal(expected: 4, actual: actual.Count(FaceCard.Queen));
        Assert.Equal(expected: 4, actual: actual.Count(FaceCard.Jack));
        Assert.Equal(expected: 4, actual: actual.Count(FaceCard.Ten));
        Assert.Equal(expected: 5, actual: actual.Count(Suit.Clubs));
        Assert.Equal(expected: 5, actual: actual.Count(Suit.Diamonds));
        Assert.Equal(expected: 5, actual: actual.Count(Suit.Hearts));
        Assert.Equal(expected: 5, actual: actual.Count(Suit.Spades));
        Assert.True(actual.Contains(FaceCard.Ace));
        Assert.True(actual.Contains(FaceCard.King));
        Assert.True(actual.Contains(FaceCard.Queen));
        Assert.True(actual.Contains(FaceCard.Jack));
        Assert.True(actual.Contains(FaceCard.Ten));
    }

    [Fact]
    public void CannotCreateTableWithNullHandsTest() =>
        Assert.Throws<ArgumentNullException>(() => new Table(null!));

    [Fact]
    public void CannotCreateTableWithNotUniqueCardsTest()
    {
        // arrange
        var hands = new[]
        {
            new Hand(new[] {new Card(FaceCard.Ace, Suit.Hearts)}),
            new Hand(new[] {new Card(FaceCard.Ace, Suit.Hearts)})
        };

        // act, assert
        Assert.Throws<ArgumentException>(() =>
            new Table(hands));
    }

    [Fact]
    public void CannotCreateTableWithLessThenTwoPlayersHandsTest()
    {
        // arrange
        var onlyOneHand = new Hand[]
        {
            new(new Card[]
            {
                new(FaceCard.Ace, Suit.Clubs),
                new(FaceCard.King, Suit.Clubs),
                new(FaceCard.Queen, Suit.Clubs)
            })
        };

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Table(onlyOneHand));
    }

    [Fact]
    public void CannotCreateTableWithMoreThenFourPlayersHandsTest()
    {
        // arrange
        var fiveHands = new Hand[]
        {
            new(new Card[] {new(FaceCard.Ace, Suit.Clubs)}),
            new(new Card[] {new(FaceCard.King, Suit.Diamonds)}),
            new(new Card[] {new(FaceCard.Queen, Suit.Hearts)}),
            new(new Card[] {new(FaceCard.Jack, Suit.Spades)}),
            new(new Card[] {new(FaceCard.Ten, Suit.Spades)})
        };

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new Table(fiveHands));
    }
}