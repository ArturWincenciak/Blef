using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GameplaysRepository : IGameplaysRepository
{
    private readonly Dictionary<GameId, GameplayProjection> _gameplays = new();

    public void Add(GameplayProjection gameplayProjection) =>
        _gameplays.Add(gameplayProjection.Id, gameplayProjection);

    public GameplayProjection Get(GameId gameId) =>
        _gameplays[gameId];
}