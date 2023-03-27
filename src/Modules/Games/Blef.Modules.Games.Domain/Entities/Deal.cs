using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    public DealId DealId { get; }
    private readonly IEnumerable<DealPlayer> _players;
    private readonly BidHistory _bidHistory;

    private Deal(DealId dealId, IEnumerable<DealPlayer> players)
    {
        DealId = dealId;
        _players = players;
        _bidHistory = new();
    }

    public static Deal Create(DealId dealId, IEnumerable<NewDealPlayer> players) =>
        new(dealId, CreatePlayers(players));

    public IEnumerable<Card> GetCards(PlayerId playerId)
    {
        var player = _players.Single(p => p.Id.Equals(playerId));
        return player.GetCards();
    }

    public void Bid(PlayerId playerId, PokerHand bid)
    {
        // todo ...
        _bidHistory.OnBid(playerId, bid);
    }

    public LooserPlayer Check(PlayerId playerId)
    {
        // todo: ...
        return new(playerId);
    }

    public DealFlowResult GetDealFlow()
    {
        var bids = _bidHistory.GetFlow();

        PlayerId checkingPlayerId = null;
        PlayerId looserPlayerId = null;

        return new DealFlowResult(_players, bids, checkingPlayerId, looserPlayerId);
    }

    private static IEnumerable<DealPlayer> CreatePlayers(IEnumerable<NewDealPlayer> players) =>
        players.Select(CreatePlayer);

    private static DealPlayer CreatePlayer(NewDealPlayer p) =>
        new(p.PlayerId, DealCards(p.CardsAmount));

    private static IEnumerable<Card> DealCards(CardsAmount cardsAmount)
    {
        // todo: add number of cards parameter based on previous game
        // todo: randomise cards
        return new[]
        {
            new Card(FaceCard.Ace, Suit.Clubs)
        };
    }
}