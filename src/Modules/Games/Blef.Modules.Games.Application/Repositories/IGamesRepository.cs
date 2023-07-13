using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Application.Repositories;

internal interface IGamesRepository
{
    Task Add(Game game);
    Task<Game?> Get(GameId gameId);
}