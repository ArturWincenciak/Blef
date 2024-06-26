﻿using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model.PokerHands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Domain.Model;

internal sealed class Gameplay
{
    public enum DealStatus
    {
        [UsedImplicitly]
        InProgress = 1,
        Finished = 2
    }

    public enum GameStatus
    {
        [UsedImplicitly]
        JoiningPlayers = 1,
        InProgress = 2,
        GameIsOver = 4
    }

    private readonly Dictionary<DealNumber, DealDetails> _deals = new();
    private readonly List<PlayerEntry> _gamePlayers = new();

    private GamePlayer? _winner;
    public GameId Id { get; }

    private IReadOnlyCollection<PlayerEntry> GamePlayerEntries =>
        _gamePlayers;

    private GameStatus Status
    {
        get
        {
            if (_deals.Count == 0)
                return GameStatus.JoiningPlayers;

            return _winner is not null
                ? GameStatus.GameIsOver
                : GameStatus.InProgress;
        }
    }

    public Gameplay(GameId id) =>
        Id = id;

    private IReadOnlyCollection<DealSummary> Deals() =>
        _deals
            .Select(deal => new DealSummary(
                deal.Key,
                Status: deal.Value.DealResolution is not null
                    ? DealStatus.Finished
                    : DealStatus.InProgress,
                deal.Value.DealResolution))
            .ToArray();

    public void OnPlayerJoined(GamePlayer gamePlayer) =>
        _gamePlayers.Add(new(gamePlayer, JoiningOrder: _gamePlayers.Count + 1));

    public void OnDealStarted(DealNumber dealNumber, IReadOnlyCollection<DealPlayer> dealPlayers) =>
        _deals.Add(dealNumber, value: new(dealPlayers, Bids: new()));

    public void OnBidPlaced(DealNumber dealNumber, PlayerId playerId, PokerHand pokerHand)
    {
        var deal = _deals[dealNumber];
        var bid = new Bid(pokerHand, playerId);
        var order = deal.Bids.Count + 1;
        deal.Bids.Add(new(order, bid));
    }

    public void OnCheckPlaced(DealNumber dealNumber, CheckingPlayer checkingPlayer, LooserPlayer looserPlayer)
    {
        var deal = _deals[dealNumber];
        _deals[dealNumber] = deal with
        {
            DealResolution = new(checkingPlayer, looserPlayer)
        };
    }

    public void OnGameFinished(GamePlayer winner) =>
        _winner = winner;

    public Game GetGameProjection() =>
        new(Status, GamePlayerEntries, Deals: Deals(), _winner);

    public DealDetails? GetDealProjection(DealNumber dealNumber) =>
        _deals.Any(deal => deal.Key == dealNumber)
            ? _deals[dealNumber]
            : null;

    public IEnumerable<Card> GetHand(DealNumber dealNumber, PlayerId playerId)
    {
        if (!_deals.TryGetValue(dealNumber, value: out var deal))
            throw new DealNotFoundException(Id.Id, dealNumber.Number);

        if (_gamePlayers.TrueForAll(player => player.Player.Id != playerId))
            throw new PlayerNotJoinedTheGameException(Id, playerId);

        var gamePlayer = _gamePlayers.Single(player => player.Player.Id == playerId);
        if (gamePlayer.Player.IsInTheGame == false)
            throw new PlayerAlreadyLostTheGameException(Id, playerId);

        var dealPlayer = deal.Players.Single(player => player.Player == playerId);
        return dealPlayer.Hand.Cards;
    }

    internal sealed record Game(
        GameStatus Status,
        IReadOnlyCollection<PlayerEntry> GamePlayers,
        IReadOnlyCollection<DealSummary> Deals,
        GamePlayer? Winner);

    internal sealed record DealSummary(
        DealNumber Number,
        DealStatus Status,
        DealResolution? DealResolution);

    internal sealed record DealResolution(
        CheckingPlayer CheckingPlayer,
        LooserPlayer Looser);

    internal sealed record PlayerEntry(
        GamePlayer Player,
        int JoiningOrder);

    internal sealed record DealDetails(
        IReadOnlyCollection<DealPlayer> Players,
        List<BidRecord> Bids,
        DealResolution? DealResolution = null);

    internal sealed record BidRecord(
        int Order,
        Bid Bid);
}