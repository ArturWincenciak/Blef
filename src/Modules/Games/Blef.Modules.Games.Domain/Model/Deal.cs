using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;

namespace Blef.Modules.Games.Domain.Model;

internal sealed class Deal
{
    private readonly MoveOrderPolicy _moveOrderPolicy;
    private readonly DealPlayersSet _playersSet;
    private bool _dealIsOver;
    private Bid? _lastBid;

    public DealId Id { get; }

    private bool BetHasBeenMade =>
        _lastBid is not null;

    public Deal(DealId dealId, DealSet dealSet)
    {
        if (dealSet is null)
            throw new ArgumentNullException(nameof(dealSet));

        Id = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _playersSet = dealSet.PlayersSet;
        _moveOrderPolicy = new MoveOrderPolicy(dealSet.MoveSequence);
        _lastBid = null;
        _dealIsOver = false;
    }

    public void Bid(Bid newBid)
    {
        if (_dealIsOver)
            throw new InvalidOperationException("Deal is already over");

        _moveOrderPolicy.Move(newBid.Player);

        if (BetHasBeenMade && newBid.PokerHand.IsBetterThan(_lastBid!.PokerHand) == false)
            throw new BidIsNotHigherThenLastOneException(Id, newBid, _lastBid);

        _lastBid = newBid;
    }

    public LooserPlayer Check(CheckingPlayer checkingPlayerId)
    {
        if (_dealIsOver)
            throw new InvalidOperationException("Deal is already over");

        _moveOrderPolicy.Move(checkingPlayerId.Player);

        if (BetHasBeenMade == false)
            throw new NoBidToCheckException(Id);

        _dealIsOver = true;

        return _lastBid!.PokerHand.IsOnTable(_playersSet.Table())
            ? new LooserPlayer(checkingPlayerId.Player)
            : new LooserPlayer(_lastBid.Player);
    }
}