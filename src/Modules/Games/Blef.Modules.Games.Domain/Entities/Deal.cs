using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly BidHistory _bidHistory;
    private readonly IEnumerable<DealPlayer> _players;

    private PokerHand _bid;
    private CheckingPlayer _checkingPlayer;
    private LooserPlayer _looserPlayer;
    public DealId DealId { get; }

    public Deal(DealId dealId, IEnumerable<DealPlayer> players)
    {
        // todo: validate if here are at least two players
        // todo: validate if here are not more then four players
        DealId = dealId;
        _players = players;
        _bidHistory = new BidHistory();
        _looserPlayer = new LooserPlayer();
        _checkingPlayer = new CheckingPlayer();
    }

    public IEnumerable<Card> GetCards(PlayerId playerId)
    {
        var player = _players.Single(p => p.Id.Equals(playerId));
        return player.GetCards();
    }

    public void Bid(PlayerId playerId, PokerHand bid)
    {
        // todo: check if new bid is higher then last bid
        _bid = bid;
        _bidHistory.OnBid(playerId, bid);
    }

    public LooserPlayer Check(PlayerId playerId)
    {
        // todo: check if last bid exists in all player hands
        _checkingPlayer = new CheckingPlayer(playerId.Id);
        _looserPlayer = new LooserPlayer(playerId.Id);
        return _looserPlayer;
    }

    public DealFlowResult GetDealFlow()
    {
        var bids = _bidHistory.GetFlow();
        return new DealFlowResult(_players, bids, _checkingPlayer, _looserPlayer);
    }
}