using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GameplaysRepository : IGameplaysRepository
{
    private readonly Dictionary<Guid, GameplayProjection> _gameplays = new();

    public void Add(GameplayProjection gameplayProjection) =>
        _gameplays.Add(gameplayProjection.Id, gameplayProjection);

    public GameplayProjection Get(Guid gameId) =>
        _gameplays[gameId];
}