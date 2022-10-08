using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Repositories;

internal interface IGamesRepository
{
    void Add(Game game);
    Game Get(Guid gameId);
}