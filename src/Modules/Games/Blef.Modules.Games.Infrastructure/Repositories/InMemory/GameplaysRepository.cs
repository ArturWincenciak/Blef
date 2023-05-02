using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GameplaysRepository : IGameplaysRepository
{
    private readonly Dictionary<Guid, Gameplay> _gameplays = new();

    public void Add(Gameplay gameplay) =>
        _gameplays.Add(gameplay.Id, gameplay);

    public Gameplay Get(Guid gameId) =>
        _gameplays[gameId];
}