using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.Tests.Mocks;

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
    public void Given_TwoPlayersWithOneCardEachInDealWithCroupier_When_DealCards_Then_PlayersHaveProperCards()
    {
        // arrange
        var deckFactory = new DeckFactoryMock();
        var croupier = new Croupier(deckFactory);
        var playerId1 = Guid.Parse("1F2608EC-1479-409B-917A-10F4995213F9");
        var playerId2 = Guid.Parse("6619A981-6DB6-4806-AEDD-A01911273CA6");

        // act
        var dealSet = croupier.Deal(
            new(new NextDealPlayer[]
            {
                new(Player: new(playerId1), CardsAmount.Initial, Order: Order.Create(1)),
                new(Player: new(playerId2), CardsAmount.Initial, Order: Order.Create(2))
            }));
        var hand1 = GetHand(dealSet, playerId: new(playerId1));
        var hand2 = GetHand(dealSet, playerId: new(playerId2));

        // assert
        Assert.Equal(expected: TakeCards(from: 1, amount: 1), hand1.Cards);
        Assert.Equal(expected: TakeCards(from: 2, amount: 1), hand2.Cards);
    }

    [Fact]
    public void Given_ThreePlayersWithFewCardsInDealWithCroupier_When_DealCards_Then_PlayersHaveProperCards()
    {
        // arrange
        var deckFactory = new DeckFactoryMock();
        var croupier = new Croupier(deckFactory);
        var playerId1 = Guid.Parse("EC4EEBAF-BFB2-4035-9E0A-E7EFDDC98262");
        var playerId2 = Guid.Parse("61A8DD76-60B4-4255-9F79-5D00D1A0BA6B");
        var playerId3 = Guid.Parse("E0B517EE-F725-4A0C-B326-6FD42CE97E33");

        // act
        var dealSet = croupier.Deal(
            new(new NextDealPlayer[]
            {
                new(Player: new(playerId1), CardsAmount.Initial, Order: Order.Create(1)),
                new(Player: new(playerId2), CardsAmount.Max, Order: Order.Create(2)),
                new(Player: new(playerId3), CardsAmount: CardsAmount.Initial.AddOneCard().AddOneCard(),
                    Order: Order.Create(3))
            }));
        var hand1 = GetHand(dealSet, playerId: new(playerId1));
        var hand2 = GetHand(dealSet, playerId: new(playerId2));
        var hand3 = GetHand(dealSet, playerId: new(playerId3));

        // assert
        Assert.Equal(expected: TakeCards(from: 1, amount: 1), hand1.Cards);
        Assert.Equal(expected: TakeCards(from: 2, amount: 5), hand2.Cards);
        Assert.Equal(expected: TakeCards(from: 7, amount: 3), hand3.Cards);
    }

    [Fact]
    public void Given_FourPlayersWithFiveCardsEachInDealWithCroupier_When_DealCards_Then_PlayersHaveProperCards()
    {
        // arrange
        var deckFactory = new DeckFactoryMock();
        var croupier = new Croupier(deckFactory);
        var playerId1 = Guid.Parse("A18D34F1-5B26-478E-9907-9F4CE84F03A5");
        var playerId2 = Guid.Parse("41596274-C1FE-4314-90C3-9ED5F21EBB89");
        var playerId3 = Guid.Parse("A31ECC8C-0C6F-4890-958B-407B5ECC5F87");
        var playerId4 = Guid.Parse("392D2B74-5B97-461E-B20C-70807E2D7778");

        // act
        var dealSet = croupier.Deal(
            new(new NextDealPlayer[]
            {
                new(Player: new(playerId1), CardsAmount.Max, Order: Order.Create(1)),
                new(Player: new(playerId2), CardsAmount.Max, Order: Order.Create(2)),
                new(Player: new(playerId3), CardsAmount.Max, Order: Order.Create(3)),
                new(Player: new(playerId4), CardsAmount.Max, Order: Order.Create(4))
            }));
        var hand1 = GetHand(dealSet, playerId: new(playerId1));
        var hand2 = GetHand(dealSet, playerId: new(playerId2));
        var hand3 = GetHand(dealSet, playerId: new(playerId3));
        var hand4 = GetHand(dealSet, playerId: new(playerId4));

        // assert
        Assert.Equal(expected: TakeCards(from: 1, amount: 5), hand1.Cards);
        Assert.Equal(expected: TakeCards(from: 6, amount: 5), hand2.Cards);
        Assert.Equal(expected: TakeCards(from: 11, amount: 5), hand3.Cards);
        Assert.Equal(expected: TakeCards(from: 16, amount: 5), hand4.Cards);
    }

    [Fact]
    public void Given_TooManyNextDealPlayers_When_CreateNextDealPlayerSet_Then_ShouldThrowException()
    {
        // arrange
        var tooManyPlayers = new NextDealPlayer[]
        {
            new(Player: new(Guid.Parse("F6EC0DAD-D2C9-493C-95CD-543DC76E54BA")), CardsAmount.Initial,
                Order: Order.Create(1)),
            new(Player: new(Guid.Parse("F5AF983D-C5E2-4D95-8B46-9726066B5435")), CardsAmount.Initial,
                Order: Order.Create(2)),
            new(Player: new(Guid.Parse("C3D88B45-BFDB-4ADD-8925-08E068A15167")), CardsAmount.Initial,
                Order: Order.Create(3)),
            new(Player: new(Guid.Parse("64C4BC35-A477-42D5-91B5-FA073E03C7AD")), CardsAmount.Initial,
                Order: Order.Create(4)),
            new(Player: new(Guid.Parse("ADD712CF-818E-408B-8D72-DFAB518F48D9")), CardsAmount.Initial,
                Order: Order.Create(1))
        };

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new NextDealPlayersSet(tooManyPlayers));
    }

    private static Card[] TakeCards(int from, int amount) =>
        DeckFactoryMock.Cards
            .Take(new Range(start: from - 1, end: (from - 1) + amount))
            .Select(card => new Card(card.FaceCard, card.Suit))
            .ToArray();

    private static Hand GetHand(DealSet dealSet, PlayerId playerId) =>
        dealSet.PlayersSet.Players.Single(player => player.Player == playerId).Hand;
}