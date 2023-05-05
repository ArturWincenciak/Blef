using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
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
        var gameplay = _gameplaysRepository.Get(query.Game);
        var gameProjection = gameplay.GetGameProjection();
        var result = Map(gameProjection);
        return result;
    }

    private GetGame.Result Map(GameplayProjection.GameProjection gameProjection) =>
        new(Map(gameProjection.Status),
            Map(gameProjection.GamePlayers),
            Map(gameProjection.DealNumbers),
            Map(gameProjection.Winner));

    private GetGame.GameStatus Map(GameplayProjection.GameStatus gameStatus) =>
        new(gameStatus.ToString());

    private static IEnumerable<GetGame.Player> Map(IEnumerable<GamePlayer> gamePlayers) =>
        gamePlayers.Select(player => new GetGame.Player(player.Id.Id, player.Nick.Nick));

    private static IEnumerable<GetGame.DealNumber> Map(IEnumerable<DealNumber> dealNumbers) =>
        dealNumbers.Select(number => new GetGame.DealNumber(number.Number));

    private static GetGame.Winner Map(GamePlayer? gamePlayer) =>
        new(gamePlayer?.Id.Id ?? Guid.Empty);
}