using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GamesRepository : IGamesRepository
{
    private readonly Dictionary<Guid, Game> _games = new();

    public Task Add(Game game)
    {
        _games.Add(game.Id.Id, game);
        return Task.CompletedTask;
    }

    public async Task<Game?> Get(GameId gameId) =>
        await Task.FromResult(_games.TryGetValue(gameId.Id, out var game) ? game : null);
}