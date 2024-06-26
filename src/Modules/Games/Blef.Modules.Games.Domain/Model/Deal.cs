﻿using Blef.Modules.Games.Domain.Exceptions;
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
        Id = dealId;
        _playersSet = dealSet.PlayersSet;
        _moveOrderPolicy = new(dealSet.MoveSequence);
        _lastBid = null;
        _dealIsOver = false;
    }

    public void Bid(Bid newBid)
    {
        if (_dealIsOver)
            throw new InvalidOperationException("Deal is already over");

        _moveOrderPolicy.CheckMoveOrder(newBid.Player);

        if (BetHasBeenMade && newBid.PokerHand.IsBetterThan(_lastBid!.PokerHand) == false)
            throw new BidIsNotHigherThenLastOneException(newBid, _lastBid);

        _moveOrderPolicy.CommitMove(newBid.Player);

        _lastBid = newBid;
    }

    public LooserPlayer Check(CheckingPlayer checkingPlayerId)
    {
        if (_dealIsOver)
            throw new InvalidOperationException("Deal is already over");

        _moveOrderPolicy.CheckMoveOrder(checkingPlayerId.Player);

        if (BetHasBeenMade == false)
            throw new NoBidToCheckException(Id);

        _moveOrderPolicy.CommitMove(checkingPlayerId.Player);
        _dealIsOver = true;

        return _lastBid!.PokerHand.IsOnTable(_playersSet.Table())
            ? new(checkingPlayerId.Player)
            : new LooserPlayer(_lastBid.Player);
    }
}