using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Application.Repositories;

internal interface IGameplaysRepository
{
    void Add(Gameplay gameplay);
    Gameplay Get(GameId gameId);
}