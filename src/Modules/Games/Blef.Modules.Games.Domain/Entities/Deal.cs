﻿using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly DealPlayersSet _playersSet;
    private Bid _lastBid;
    private CheckingPlayer _checkingPlayer;
    private LooserPlayer _looserPlayer;
    private readonly BidHistory _bidHistory;
    private readonly MoveOrderPolicy _moveOrderPolicy;

    public DealId DealId { get; }

    public Deal(DealId dealId, DealPlayersSet playersSet, MoveSequence moveSequence)
    {
        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _playersSet = playersSet;
        _bidHistory = new BidHistory();
        _looserPlayer = new LooserPlayer();
        _checkingPlayer = new CheckingPlayer();
        _moveOrderPolicy = new(moveSequence);
    }

    public Hand GetHand(PlayerId playerId)
    {
        var player = _playersSet.Players.Single(p => p.PlayerId == playerId);
        return player.Hand;
    }

    public void Bid(Bid newBid)
    {
        if (_moveOrderPolicy.CheckIfThatIsThePlayerMove(newBid.Player) == false)
            throw new ThatIsNotThisPlayerTurnNowException(newBid.Player);

        if (BetHasBeenMade)
            if (newBid.PokerHand.IsBetterThan(_lastBid.PokerHand) == false)
                throw new BidIsNotHigherThenLastOneException(DealId, newBid, _lastBid);

        _lastBid = newBid;
        _moveOrderPolicy.OnPlayerMoved(newBid.Player);
        _bidHistory.OnBid(newBid);
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        if (_moveOrderPolicy.CheckIfThatIsThePlayerMove(checkingPlayerId) == false)
            throw new ThatIsNotThisPlayerTurnNowException(checkingPlayerId);

        if (BetHasBeenMade == false)
            throw new NoBidToCheckException(DealId);

        _checkingPlayer = new CheckingPlayer(checkingPlayerId.Id);

        var allPlayersHands = _playersSet.Players
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
        return new DealFlowResult(_playersSet.Players, bids, _checkingPlayer, _looserPlayer);
    }

    private bool BetHasBeenMade =>
        _lastBid is not null;
}