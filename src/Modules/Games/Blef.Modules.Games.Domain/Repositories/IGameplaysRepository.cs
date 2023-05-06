using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Repositories;

internal interface IGameplaysRepository
{
    void Add(Gameplay gameplay);
    Gameplay Get(GameId gameId);
}