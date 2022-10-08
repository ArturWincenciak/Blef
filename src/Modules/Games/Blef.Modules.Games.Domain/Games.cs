using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;

namespace Blef.Modules.Games.Domain;

internal sealed class Games : IGamesRepository
{
    private readonly Dictionary<Guid, Game> _games = new();

    public void Add(Game game) =>
        _games.Add(game.Id, game);

    public Game Get(Guid gameId) =>
        _games[gameId];
}