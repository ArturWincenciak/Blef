using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    public DealId Id { get; }
    private readonly IEnumerable<DealPlayer> _players;
    private readonly BidHistory _bidHistory;

    private Deal(DealId id, IEnumerable<DealPlayer> players)
    {
        Id = id;
        _players = players;
        _bidHistory = new();
    }

    public static Deal Create(DealId id, IEnumerable<PlayerId> players) =>
        new(id, players.Select(p => new DealPlayer( new(p.Id), DealCards())));

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

    public void Check(PlayerId playerId)
    {
        // todo: ...
    }

    public DealFlowResult GetDealFlow()
    {
        var bids = _bidHistory.GetFlow();

        PlayerId checkingPlayerId = null;
        PlayerId looserPlayerId = null;

        return new DealFlowResult(_players, bids, checkingPlayerId, looserPlayerId);
    }

    private static IEnumerable<Card> DealCards()
    {
        // todo: add number of cards parameter based on previous game
        // todo: randomise cards
        return new[]
        {
            new Card(FaceCard.Ace, Suit.Clubs)
        };
    }
}