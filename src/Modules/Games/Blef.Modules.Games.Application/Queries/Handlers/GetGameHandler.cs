using Blef.Modules.Games.Application.Repositories;
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

    public async Task<GetGame.Result> Handle(GetGame query, CancellationToken cancellation)
    {
        var gameplay = await _gameplaysRepository.Get(query.Game);
        var gameProjection = gameplay.GetGameProjection();
        return Map(gameProjection);
    }

    private static GetGame.Result Map(Gameplay.Game game) =>
        new(Status: Map(game.Status),
            Players: Map(game.GamePlayers.ToArray()),
            Deals: Map(game.Deals.ToArray()),
            Winner: Map(game.Winner));

    private static GetGame.GameStatus Map(Gameplay.GameStatus gameStatus) =>
        new(gameStatus.ToString());

    private static IReadOnlyCollection<GetGame.Player> Map(IReadOnlyCollection<Gameplay.PlayerEntry> gamePlayers) =>
        gamePlayers
            .Select(player => new GetGame.Player(
                player.Player.Id.Id, player.Player.Nick.Nick, player.JoiningOrder))
            .ToArray();

    private static IReadOnlyCollection<GetGame.Deal> Map(IReadOnlyCollection<Gameplay.DealSummary> deals) =>
        deals
            .Select(deal => new GetGame.Deal(
                deal.Number.Number,
                State: deal.Status.ToString(),
                DealResolution: Map(deal.DealResolution)))
            .ToArray();

    private static GetGame.DealResolution Map(Gameplay.DealResolution? dealResolution) =>
        new(CheckingPlayerId: dealResolution?.CheckingPlayer.Player.Id ?? Guid.Empty,
            LooserPlayerId: dealResolution?.Looser.Player.Id ?? Guid.Empty);

    private static GetGame.Winner Map(GamePlayer? gamePlayer) =>
        new(gamePlayer?.Id.Id ?? Guid.Empty);
}