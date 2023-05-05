﻿using System.Data;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class GameplayProjection
{
    public GameId Id { get; }

    private GamePlayer? _winner = null;

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
        var bid = new Bid(pokerHand, playerId);
        deal.Bids.Add(bid);
    }

    public void OnCheckPlaced(DealNumber dealNumber, PlayerId checkingPlayer, LooserPlayer looserPlayer)
    {
        var deal = _deals[dealNumber];
        _deals[dealNumber] = deal with
        {
            CheckingPlayerId = checkingPlayer,
            LooserPlayerId = looserPlayer
        };
    }

    public GameProjection GetGameProjection() =>
        new(Status, _gamePlayers, Deals, _winner);

    public DealProjection GetDealProjection(DealNumber dealNumber) =>
        _deals[dealNumber];

    public IEnumerable<Card> GetHand(DealNumber dealNumber, PlayerId playerId)
    {
        var deal = _deals[dealNumber];
        var player = deal.Players.Single(player => player.Player == playerId);
        return player.Hand.Cards;
    }

    public void OnGameFinished(GamePlayer winner) =>
        _winner = winner;

    private IEnumerable<(DealNumber Number, DealStatus State, LooserPlayer? Looser)> Deals =>
        _deals.Select(deal => (
            Number: deal.Key,
            State: deal.Value.LooserPlayerId is not null
                ? DealStatus.Finished
                : DealStatus.InProgress,
            Looser: deal.Value.LooserPlayerId));

    private GameStatus Status
    {
        get
        {
            if (_deals.Count == 0)
                return GameStatus.JoiningPlayers;

            if (_winner is not null)
                return GameStatus.GameIsOver;

            return GameStatus.InProgress;
        }
    }

    internal sealed record GameProjection(
        GameStatus Status,
        IEnumerable<GamePlayer> GamePlayers,
        IEnumerable<(DealNumber Number, DealStatus State, LooserPlayer? Looser)> Deals,
        GamePlayer? Winner);

    internal sealed record DealProjection(
        IEnumerable<DealPlayer> Players,
        List<Bid> Bids,
        PlayerId? CheckingPlayerId = null,
        LooserPlayer? LooserPlayerId = null);

    public enum GameStatus
    {
        JoiningPlayers,
        InProgress,
        GameIsOver
    }

    public enum DealStatus
    {
        InProgress,
        Finished
    }
}