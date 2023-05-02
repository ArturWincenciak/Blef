using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Domain.Repositories;

internal interface IGameplaysRepository
{
    void Add(Gameplay gameplay);
    Gameplay Get(Guid gameId);
}