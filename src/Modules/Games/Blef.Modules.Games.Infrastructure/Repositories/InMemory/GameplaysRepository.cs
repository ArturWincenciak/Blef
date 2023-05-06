using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GameplaysRepository : IGameplaysRepository
{
    private readonly Dictionary<GameId, Gameplay> _gameplays = new();

    public void Add(Gameplay gameplay) =>
        _gameplays.Add(gameplay.Id, gameplay);

    public Gameplay Get(GameId gameId) =>
        _gameplays[gameId];
}