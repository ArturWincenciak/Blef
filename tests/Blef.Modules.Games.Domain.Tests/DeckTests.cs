using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DeckTests
{
    private static Card[] TwentyFourUniqueCards =>
        new[]
        {
            new Card(FaceCard.Ace, Suit.Diamonds),
            new Card(FaceCard.Ace, Suit.Spades),
            new Card(FaceCard.Ten, Suit.Clubs),
            new Card(FaceCard.Jack, Suit.Spades),
            new Card(FaceCard.Queen, Suit.Diamonds),
            new Card(FaceCard.King, Suit.Hearts),
            new Card(FaceCard.King, Suit.Clubs),
            new Card(FaceCard.Ace, Suit.Clubs),
            new Card(FaceCard.Queen, Suit.Clubs),
            new Card(FaceCard.Jack, Suit.Diamonds),
            new Card(FaceCard.Ten, Suit.Diamonds),
            new Card(FaceCard.King, Suit.Diamonds),
            new Card(FaceCard.Nine, Suit.Clubs),
            new Card(FaceCard.King, Suit.Spades),
            new Card(FaceCard.Queen, Suit.Spades),
            new Card(FaceCard.Jack, Suit.Clubs),
            new Card(FaceCard.Nine, Suit.Spades),
            new Card(FaceCard.Ace, Suit.Hearts),
            new Card(FaceCard.Ten, Suit.Spades),
            new Card(FaceCard.Ten, Suit.Hearts),
            new Card(FaceCard.Nine, Suit.Hearts),
            new Card(FaceCard.Nine, Suit.Diamonds),
            new Card(FaceCard.Jack, Suit.Hearts),
            new Card(FaceCard.Queen, Suit.Hearts)
        };

    [Fact]
    public void CannotCreateWithNullArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new Deck(null!));

    [Fact]
    public void DeckCannotBeCreateWithLessThenTwentyFourCardsTest()
    {
        // arrange
        var onlyOneCard = new Card[] {new(FaceCard.Ace, Suit.Clubs)};

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Deck(onlyOneCard));
    }

    [Fact]
    public void DeckCannotBeCreateWithMoreThenTwentyFourCardsTest()
    {
        // arrange
        var twentyFiveCards = CreateManyTheSameCards(25);

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Deck(twentyFiveCards));
    }

    [Fact]
    public void DeckCannotBeCreateWithNoUniqueCardsTest()
    {
        // arrange
        var twentyFourCards = CreateManyTheSameCards(24);

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new Deck(twentyFourCards));
    }

    [Fact]
    public void CreateDeckTest() =>
        Assert.Null(Record.Exception(() =>
            new Deck(TwentyFourUniqueCards)));

    [Fact]
    public void When_DealOneCard_Then_HandContainsTheFirstOneCard()
    {
        // arrange
        var deck = new Deck(AllCards());

        // act
        var actual = deck.Deal(CardsAmount.Initial);
        var expected = new Hand(TakeCards(from: 1, amount: 1));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealTwoCards_Then_HandContainsTheFirstTwoCards()
    {
        // arrange
        var deck = new Deck(AllCards());
        var twoCards = CardsAmount.Initial.AddOneCard();

        // act
        var actual = deck.Deal(twoCards);
        var expected = new Hand(TakeCards(from: 1, amount: 2));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealOneCardTwoTimes_Then_HandContainsTheSingleSecondCard()
    {
        // arrange
        var deck = new Deck(AllCards());

        // act
        _ = deck.Deal(CardsAmount.Initial);
        var actual = deck.Deal(CardsAmount.Initial);
        var expected = new Hand(TakeCards(from: 2, amount: 1));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealOneCardAndThreeCards_Then_HandContainsTheThreeCards()
    {
        // arrange
        var deck = new Deck(AllCards());
        var threeCards = CardsAmount.Initial.AddOneCard().AddOneCard();

        // act
        _ = deck.Deal(CardsAmount.Initial);
        var actual = deck.Deal(threeCards);
        var expected = new Hand(TakeCards(from: 2, amount: 3));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealOneCardAndFiveCards_Then_HandContainsTheFiveCards()
    {
        // arrange
        var deck = new Deck(AllCards());

        // act
        deck.Deal(CardsAmount.Initial);
        var actual = deck.Deal(CardsAmount.Max);
        var expected = new Hand(TakeCards(from: 2, amount: 5));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealOneCardThreeTimesAndFiveCardsAndOneCardAndAgainFiveCards_Then_HandContainsTheFiveCards()
    {
        // arrange
        var deck = new Deck(AllCards());

        // act
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Initial);
        var actual = deck.Deal(CardsAmount.Max);
        var expected = new Hand(TakeCards(from: 10, amount: 5));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealFiveCardsFourTimesAndOneCardFourTimes_Then_HandContainsTheLastOneCard()
    {
        // arrange
        var deck = new Deck(AllCards());

        // act
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);
        var actual = deck.Deal(CardsAmount.Initial);
        var expected = new Hand(TakeCards(from: 24, amount: 1));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealFiveCardsFourTimesAndOneAndTreeCards_Then_HandContainsTheLastThreeCards()
    {
        // arrange
        var deck = new Deck(AllCards());
        var threeCards = CardsAmount.Initial.AddOneCard().AddOneCard();

        // act
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Initial);
        var actual = deck.Deal(threeCards);
        var expected = new Hand(TakeCards(from: 22, amount: 3));

        // assert
        AssertTheSame(expected, actual);
    }

    [Fact]
    public void When_DealFiveCardsFiveTimes_MeansTooManyCards_Then_ThrowInvalidOperationException()
    {
        // arrange
        var deck = new Deck(AllCards());
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);

        // act, assert
        Assert.Throws<InvalidOperationException>(() => deck.Deal(CardsAmount.Max));
    }

    [Fact]
    public void When_DealTooManyCards_Then_ThrowInvalidOperationException()
    {
        // arrange
        var deck = new Deck(AllCards());
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Max);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);
        deck.Deal(CardsAmount.Initial);

        // act, assert
        Assert.Throws<InvalidOperationException>(() => deck.Deal(CardsAmount.Initial));
    }

    private static void AssertTheSame(Hand expected, Hand actual) =>
        Assert.Equal(expected.Cards, actual.Cards);

    private static Card[] CreateManyTheSameCards(int amount) =>
        Enumerable
            .Range(start: 0, amount)
            .Select(_ => new Card(FaceCard.Ace, Suit.Clubs))
            .ToArray();

    private static Card[] AllCards() =>
        TwentyFourUniqueCards
            .Select(card => new Card(card.FaceCard, card.Suit))
            .ToArray();

    private static Card[] TakeCards(int from, int amount) =>
        TwentyFourUniqueCards
            .Take(new Range(start: from - 1, end: (from - 1) + amount))
            .Select(card => new Card(card.FaceCard, card.Suit))
            .ToArray();
}