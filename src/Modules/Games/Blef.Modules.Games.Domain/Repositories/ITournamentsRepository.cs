using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Repositories;

internal interface ITournamentsRepository
{
    void Add(Tournament game);
    Tournament Get(Guid gameId);
}