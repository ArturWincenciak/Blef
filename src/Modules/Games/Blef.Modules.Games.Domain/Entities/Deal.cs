using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private readonly BidHistory _bidHistory;

    private readonly IEnumerable<DealPlayer> _players;

    private Bid _lastBid;
    private CheckingPlayer _checkingPlayer;
    private LooserPlayer _looserPlayer;

    public DealId DealId { get; }

    public Deal(DealId dealId, IEnumerable<DealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException("Deal should have at least two players");

        if (players.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException("Deal cannot have more than four players");

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _players = players;
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
        // todo: check if that is the player turn

        if(IsItFirstMoveInDeal == false)
            if(newBid.PokerHand.IsBetterThan(_lastBid.PokerHand) == false)
                throw new BidIsNotHigherThenLastOneException(DealId, newBid, _lastBid);

        _lastBid = newBid;
        _bidHistory.OnBid(newBid);
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        // todo: check if that is the player turn

        if (IsItFirstMoveInDeal)
            throw new NoBidToCheckException(DealId);

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

    private bool AreAllPlayersUnique(IEnumerable<DealPlayer> players) =>
        players
            .Select(player => player.PlayerId)
            .Distinct()
            .Count() == players.Count();
}