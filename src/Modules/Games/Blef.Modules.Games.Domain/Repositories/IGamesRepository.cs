using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Repositories;

internal interface IGamesRepository
{
    void Add(Game game);
    Game Get(GameId gameId);
}