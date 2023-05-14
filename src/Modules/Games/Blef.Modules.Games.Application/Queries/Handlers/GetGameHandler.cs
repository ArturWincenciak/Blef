﻿using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGame, GetGame.Result>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GetGameHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public Task<GetGame.Result> Handle(GetGame query, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(query.Game);
        var gameProjection = gameplay.GetGameProjection();
        return Task.FromResult(Map(gameProjection));
    }

    private GetGame.Result Map(Gameplay.Game game) =>
        new(Status: Map(game.Status),
            Players: Map(game.GamePlayers),
            Deals: Map(game.Deals),
            Winner: Map(game.Winner));

    private GetGame.GameStatus Map(Gameplay.GameStatus gameStatus) =>
        new(gameStatus.ToString());

    private static IEnumerable<GetGame.Player> Map(IEnumerable<Gameplay.PlayerEntry> gamePlayers) =>
        gamePlayers.Select(player => new GetGame.Player(
            player.Player.Id.Id, player.Player.Nick.Nick, player.JoiningOrder));

    private static IEnumerable<GetGame.Deal> Map(
        IEnumerable<Gameplay.DealSummary> deals) =>
        deals.Select(deal => new GetGame.Deal(
            deal.Number.Number,
            State: deal.Status.ToString(),
            DealResolution: Map(deal.DealResolution)));

    private static GetGame.DealResolution Map(Gameplay.DealResolution? dealResolution) =>
        new(CheckingPlayerId: dealResolution?.CheckingPlayer.Player.Id ?? Guid.Empty,
            LooserPlayerId: dealResolution?.Looser.Player.Id ?? Guid.Empty);

    private static GetGame.Winner Map(GamePlayer? gamePlayer) =>
        new(gamePlayer?.Id.Id ?? Guid.Empty);
}