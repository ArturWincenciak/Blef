using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly BidHistory _bidHistory;

    private readonly IEnumerable<DealPlayer> _players;

    private DealPlayer _previousMovingPlayer;
    private Bid _lastBid;

    private CheckingPlayer _checkingPlayer;
    private LooserPlayer _looserPlayer;

    public DealId DealId { get; }

    public Deal(DealId dealId, IEnumerable<DealPlayer> players)
    {
        if (players is null)
            throw new ArgumentNullException(nameof(players));

        if (players.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(players), "Deal should have at least two players");

        if (players.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException(nameof(players), "Deal cannot have more than four players");

        if (AreAllPlayersUnique(players) == false)
            throw new ArgumentException("No player duplicates are allowed");

        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _players = players;
        _bidHistory = new BidHistory();
        _looserPlayer = new LooserPlayer();
        _checkingPlayer = new CheckingPlayer();
        _previousMovingPlayer = null;
    }

    public Hand GetHand(PlayerId playerId)
    {
        var player = _players.Single(p => p.PlayerId == playerId);
        return player.Hand;
    }

    public void Bid(Bid newBid)
    {
        if(CheckIfThatIsThePlayerMove(newBid.Player) == false)
            throw new ThatIsNotThisPlayerTurnNowException(newBid.Player);

        if(BetHasBeenMade)
            if(newBid.PokerHand.IsBetterThan(_lastBid.PokerHand) == false)
                throw new BidIsNotHigherThenLastOneException(DealId, newBid, _lastBid);

        _lastBid = newBid;
        _previousMovingPlayer = _players.Single(player => player.PlayerId == newBid.Player);
        _bidHistory.OnBid(newBid);
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        if (CheckIfThatIsThePlayerMove(checkingPlayerId) == false)
            throw new ThatIsNotThisPlayerTurnNowException(checkingPlayerId);

        if (BetHasBeenMade == false)
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

    private bool BetHasBeenMade =>
        _lastBid is not null;

    private bool AreAllPlayersUnique(IEnumerable<DealPlayer> players) =>
        players
            .Select(player => player.PlayerId)
            .Distinct()
            .Count() == players.Count();

    private bool CheckIfThatIsThePlayerMove(PlayerId playerId)
    {
        var isThatFirstMoveInDeal = _previousMovingPlayer is null;
        if (isThatFirstMoveInDeal)
        {
            var firstPlayerInSequence = _players.Single(player => player.MoveOrder == 1);
            return firstPlayerInSequence.PlayerId == playerId;
        }

        var lastPlayerInSequenceExecutedPreviousMove = _previousMovingPlayer.MoveOrder == _players.Count();
        if (lastPlayerInSequenceExecutedPreviousMove)
        {
            var firstPlayerInSequence = _players.Single(player => player.MoveOrder == 1);
            return firstPlayerInSequence.PlayerId == playerId;
        }

        var nextSequenceNumber = _previousMovingPlayer.MoveOrder + 1;
        var nextPlayerInSequence = _players.Single(player => player.MoveOrder == nextSequenceNumber);
        return nextPlayerInSequence.PlayerId == playerId;
    }
}