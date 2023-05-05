﻿using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly MoveOrderPolicy _moveOrderPolicy;
    private readonly DealPlayersSet _playersSet;
    private Bid? _lastBid;
    private bool _dealIsOver;

    public DealId Id { get; }

    public Deal(DealId dealId, DealSet dealSet)
    {
        if (dealSet is null)
            throw new ArgumentNullException(nameof(dealSet));

        Id = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _playersSet = dealSet.PlayersSet;
        _moveOrderPolicy = new(dealSet.MoveSequence);
        _lastBid = null;
        _dealIsOver = false;
    }

    public void Bid(Bid newBid)
    {
        if (_dealIsOver)
            throw new InvalidOperationException("Deal is already over");

        _moveOrderPolicy.Move(newBid.Player);

        if (BetHasBeenMade)
            if (newBid.PokerHand.IsBetterThan(_lastBid!.PokerHand) == false)
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

         return _lastBid!.PokerHand.IsOnTable(_playersSet.Table)
            ? new LooserPlayer(checkingPlayerId.Player)
            : new LooserPlayer(_lastBid.Player);
    }

    private bool BetHasBeenMade =>
        _lastBid is not null;
}