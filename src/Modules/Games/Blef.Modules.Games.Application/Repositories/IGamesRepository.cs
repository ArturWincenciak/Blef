using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Application.Repositories;

internal interface IGamesRepository
{
    void Add(Game game);
    Game Get(GameId gameId);
}