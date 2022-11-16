using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;

namespace Blef.Modules.Games.Domain.Tests.Application.Repositories.InMemory;

internal sealed class InMemoryGamesRepository : IGamesRepository
{
    private readonly Dictionary<Guid, Game> _games = new();

    public void Add(Game game) =>
        _games.Add(game.Id, game);

    public Game Get(Guid gameId) =>
        _games[gameId];
}