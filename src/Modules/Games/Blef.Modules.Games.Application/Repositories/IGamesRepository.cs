using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Application.Repositories;

internal interface IGamesRepository
{
    void Add(Game game);
    Game Get(GameId gameId);
}