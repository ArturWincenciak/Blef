using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GameplaysRepository : IGameplaysRepository
{
    private readonly Dictionary<GameId, Gameplay> _gameplays = new();

    public Task Add(Gameplay gameplay)
    {
        _gameplays.Add(gameplay.Id, gameplay);
        return Task.CompletedTask;
    }

    public Task<Gameplay> Get(GameId gameId) =>
        Task.FromResult(_gameplays[gameId]);
}