using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class GameplayProjection
{
    public GameId Id { get; }

    private readonly List<GamePlayer> _gamePlayers = new();
    private readonly Dictionary<DealNumber, DealProjection> _deals = new();

    public GameplayProjection(GameId id) =>
        Id = id;

    public void OnPlayerJoined(GamePlayer gamePlayer) =>
        _gamePlayers.Add(gamePlayer);

    public void OnDealStarted(DealNumber dealNumber, IEnumerable<DealPlayer> dealPlayers) =>
        _deals.Add(dealNumber, new(dealPlayers, new()));

    public void OnBidPlaced(DealNumber dealNumber, PlayerId playerId, PokerHand pokerHand)
    {
        var deal = _deals[dealNumber];
        var bidNumber = deal.Bids.Count + 1;
        var bid = new Bid(pokerHand, playerId);
        deal.Bids.Add(bid);
    }

    public void OnCheckPlaced(DealNumber dealNumber, PlayerId checkingPlayerId, LooserPlayer looserPlayerId)
    {
        var deal = _deals[dealNumber];
        _deals[dealNumber] = deal with {
            CheckingPlayerId = checkingPlayerId,
            LooserPlayerId = looserPlayerId
        };
    }

    public GameProjection GetGameProjection() =>
        new(_gamePlayers, _deals.Keys);

    public DealProjection GetDealProjection(DealNumber dealNumber) =>
        _deals[dealNumber];

    public IEnumerable<Card> GetHand(DealNumber dealNumber, PlayerId playerId)
    {
        var deal = _deals[dealNumber];
        var player = deal.Players.Single(player => player.Player == playerId);
        return player.Hand.Cards;
    }

    internal sealed record GameProjection(
        IEnumerable<GamePlayer> GamePlayers,
        IEnumerable<DealNumber> DealNumbers);

    internal sealed record DealProjection(
        IEnumerable<DealPlayer> Players,
        List<Bid> Bids,
        PlayerId? CheckingPlayerId = null,
        LooserPlayer? LooserPlayerId = null);
}