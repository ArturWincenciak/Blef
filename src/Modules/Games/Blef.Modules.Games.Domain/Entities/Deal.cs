using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly BidHistory _bidHistory;

    private readonly IEnumerable<DealPlayer> _players;

    private Bid _lastBid;
    private CheckingPlayer _checkingPlayer;
    private LooserPlayer _looserPlayer;

    public DealId DealId { get; }

    public Deal(DealId dealId, IEnumerable<DealPlayer> players)
    {
        // todo: validate if here are at least two players
        // todo: validate if here are not more then four players

        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _players = players ?? throw new ArgumentNullException(nameof(players));
        _bidHistory = new BidHistory();
        _looserPlayer = new LooserPlayer();
        _checkingPlayer = new CheckingPlayer();
    }

    public Hand GetHand(PlayerId playerId)
    {
        var player = _players.Single(p => p.PlayerId == playerId);
        return player.Hand;
    }

    public void Bid(Bid newBid)
    {
        if(IsItFirstMoveInDeal == false)
            if(newBid.PokerHand.IsBetterThan(_lastBid.PokerHand) == false)
                throw new Exception("TBD"); // todo: exception

        _lastBid = newBid;
        _bidHistory.OnBid(newBid);
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        if (IsItFirstMoveInDeal) // todo: exception
            throw new Exception("TBD: First must be bid");

        _checkingPlayer = new CheckingPlayer(checkingPlayerId.Id);

        var allPlayersHands = _players
            .Select(player => player.Hand)
            .ToArray();
        var table = new Table(allPlayersHands);

        if (_lastBid.PokerHand.IsOnTable(table))
            _looserPlayer = new LooserPlayer(checkingPlayerId.Id);
        else
            _looserPlayer = new LooserPlayer(_lastBid.Player.Id);

        return _looserPlayer;
    }

    public DealFlowResult GetDealFlow()
    {
        var bids = _bidHistory.GetFlow();
        return new DealFlowResult(_players, bids, _checkingPlayer, _looserPlayer);
    }

    private bool IsItFirstMoveInDeal =>
        _lastBid is null;
}