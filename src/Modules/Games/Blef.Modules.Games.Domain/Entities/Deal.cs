using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class Deal
{
    private readonly DealPlayersSet _playersSet;
    private Bid _lastBid;
    private LooserPlayer _looserPlayer;
    private readonly MoveOrderPolicy _moveOrderPolicy;

    public DealId DealId { get; }

    public Deal(DealId dealId, DealSet dealSet)
    {
        if (dealSet is null)
            throw new ArgumentNullException(nameof(dealSet));

        DealId = dealId ?? throw new ArgumentNullException(nameof(dealId));
        _playersSet = dealSet.PlayersSet;
        _looserPlayer = new();
        _moveOrderPolicy = new(dealSet.MoveSequence);
    }

    public Hand GetHand(PlayerId playerId) =>
        _playersSet.GetHand(playerId);

    public void Bid(Bid newBid)
    {
        _moveOrderPolicy.Move(newBid.Player);

        if (BetHasBeenMade)
            if (newBid.PokerHand.IsBetterThan(_lastBid.PokerHand) == false)
                throw new BidIsNotHigherThenLastOneException(DealId, newBid, _lastBid);

        _lastBid = newBid;
    }

    public LooserPlayer Check(PlayerId checkingPlayerId)
    {
        _moveOrderPolicy.Move(checkingPlayerId);

        if (BetHasBeenMade == false)
            throw new NoBidToCheckException(DealId);

        _looserPlayer = _lastBid.PokerHand.IsOnTable(_playersSet.Table)
            ? new LooserPlayer(checkingPlayerId.Id)
            : new LooserPlayer(_lastBid.Player.Id);

        return _looserPlayer;
    }

    private bool BetHasBeenMade =>
        _lastBid is not null;
}