using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly MoveOrderPolicy _moveOrderPolicy;
    private readonly DealPlayersSet _playersSet;
    private Bid? _lastBid;
    private bool _dealIsOver;

    public DealId DealId { get; }

    public Deal(DealId dealId, DealSet dealSet)
    {
        if (dealSet is null)
            throw new ArgumentNullException(nameof(dealSet));

        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _playersSet = dealSet.PlayersSet;
        _moveOrderPolicy = new(dealSet.MoveSequence);
        _lastBid = null;
        _dealIsOver = false;
    }

    public Hand GetHand(PlayerId playerId) =>
        _playersSet.GetHand(playerId);

    public void Bid(Bid newBid)
    {
        if (_dealIsOver)
            throw new InvalidOperationException("Deal is already over");

        _moveOrderPolicy.Move(newBid.PlayerId);

        if (BetHasBeenMade)
            if (newBid.PokerHand.IsBetterThan(_lastBid!.PokerHand) == false)
                throw new BidIsNotHigherThenLastOneException(DealId, newBid, _lastBid);

        _lastBid = newBid;
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        _moveOrderPolicy.Move(checkingPlayerId);

        if (BetHasBeenMade == false)
            throw new NoBidToCheckException(DealId);

        _dealIsOver = true;

         return _lastBid!.PokerHand.IsOnTable(_playersSet.Table)
            ? new LooserPlayer(checkingPlayerId)
            : new LooserPlayer(_lastBid.PlayerId);
    }

    private bool BetHasBeenMade =>
        _lastBid is not null;
}