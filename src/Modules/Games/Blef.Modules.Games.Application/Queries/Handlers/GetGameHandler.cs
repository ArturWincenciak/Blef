using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Builder;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGame, GetGame.Result>
{
    private readonly IGameplaysRepository _gameplaysRepository;

    public GetGameHandler(IGameplaysRepository gameplaysRepository) =>
        _gameplaysRepository = gameplaysRepository;

    public async Task<GetGame.Result> Handle(GetGame query, CancellationToken cancellation)
    {
        var gameplay = _gameplaysRepository.Get(query.Game);
        var gameProjection = gameplay.GetGameProjection();
        var result = Map(gameProjection);
        return result;
    }

    private GetGame.Result Map(Gameplay.Game game) =>
        new(Map(game.Status),
            Map(game.GamePlayers),
            Map(game.Deals),
            Map(game.Winner));

    private GetGame.GameStatus Map(Gameplay.GameStatus gameStatus) =>
        new(gameStatus.ToString());

    private static IEnumerable<GetGame.Player> Map(IEnumerable<Gameplay.PlayerEntry> gamePlayers) =>
        gamePlayers.Select(player => new GetGame.Player(
            player.Player.Id.Id, player.Player.Nick.Nick, player.JoiningOrder));

    private static IEnumerable<GetGame.Deal> Map(
        IEnumerable<Gameplay.DealSummary> deals) =>
        deals.Select(deal => new GetGame.Deal(
            Number: deal.Number.Number,
            State: deal.Status.ToString(),
            DealResolution: Map(deal.DealResolution)));

    private static GetGame.DealResolution Map(Gameplay.DealResolution? dealResolution) =>
        new(dealResolution?.CheckingPlayer.Player.Id ?? Guid.Empty,
            dealResolution?.Looser.Player.Id ?? Guid.Empty);

    private static GetGame.Winner Map(GamePlayer? gamePlayer) =>
        new(gamePlayer?.Id.Id ?? Guid.Empty);
}