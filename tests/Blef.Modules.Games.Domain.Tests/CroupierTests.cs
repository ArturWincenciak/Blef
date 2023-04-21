using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class CroupierTests
{
    [Fact]
    public void CreateCroupierTest() =>
        Assert.Null(Record.Exception(() =>
        {
            var deckFactory = new DeckFactoryMock();
            return new Croupier(deckFactory);
        }));

    [Fact]
    public void Given_ThreePlayersInDealWithCroupier_When_DealCards_Then_PlayersHaveProperCards()
    {
        // arrange
        var deckFactory = new DeckFactoryMock();
        var croupier = new Croupier(deckFactory);
        var gameId = Guid.Parse("B77123B8-D23B-4502-B863-1D242D4AF807");
        var playerId_1 = Guid.Parse("EC4EEBAF-BFB2-4035-9E0A-E7EFDDC98262");
        var playerId_2 = Guid.Parse("61A8DD76-60B4-4255-9F79-5D00D1A0BA6B");
        var playerId_3 = Guid.Parse("E0B517EE-F725-4A0C-B326-6FD42CE97E33");

        // act
        var deal = croupier.Deal(
            new DealId(
                new GameId(gameId),
                new DealNumber(1)),
            new NextDealPlayer[]
            {
                new (new PlayerId(playerId_1), CardsAmount.Initial),
                new (new PlayerId(playerId_2), CardsAmount.Max),
                new (new PlayerId(playerId_3), CardsAmount.Initial.AddOneCard().AddOneCard())
            });
        var hand_1 = deal.GetHand(new PlayerId(playerId_1));
        var hand_2 = deal.GetHand(new PlayerId(playerId_2));
        var hand_3 = deal.GetHand(new PlayerId(playerId_3));

        // assert
        Assert.True(new DealId(new GameId(gameId), new DealNumber(1)) == deal.DealId);
        Assert.Equal(TakeCards(from: 1, amount: 1), hand_1.Cards);
        Assert.Equal(TakeCards(from: 2, amount: 5), hand_2.Cards);
        Assert.Equal(TakeCards(from: 7, amount: 3), hand_3.Cards);
    }

    private static Card[] TakeCards(int from, int amount) =>
        DeckFactoryMock.Cards
            .Take(new Range(from - 1, (from - 1) + amount))
            .Select(card => new Card(card.FaceCard, card.Suit))
            .ToArray();

    private class DeckFactoryMock : IDeckFactory
    {
        public Deck Create() =>
            new(Cards);

        public static Card[] Cards =>
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
    }
}