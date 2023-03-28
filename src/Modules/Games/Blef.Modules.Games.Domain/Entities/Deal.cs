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
    private LooserPlayer _looserPlayer;
    private CheckingPlayer _checkingPlayer;

    private Deal(DealId dealId, IEnumerable<DealPlayer> players)
    {
        DealId = dealId;
        _players = players;
        _bidHistory = new();
        _looserPlayer = new();
        _checkingPlayer = new();
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
        _checkingPlayer = new(playerId.Id);
        _looserPlayer = new (playerId.Id);
        return _looserPlayer;
    }

    public DealFlowResult GetDealFlow()
    {
        var bids = _bidHistory.GetFlow();
        return new DealFlowResult(_players, bids, _checkingPlayer, _looserPlayer);
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