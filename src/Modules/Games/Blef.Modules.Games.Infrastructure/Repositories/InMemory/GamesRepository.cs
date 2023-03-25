using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Infrastructure.Repositories.InMemory;

internal sealed class GamesRepository : IGamesRepository
{
    private readonly Dictionary<Guid, Game> _games = new();

    public void Add(Game game) =>
        _games.Add(game.Id.Id, game);

    public Game Get(GameId gameId) =>
        _games[gameId.Id];
}