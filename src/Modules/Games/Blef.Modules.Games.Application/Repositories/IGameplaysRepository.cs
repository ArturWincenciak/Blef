using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Application.Repositories;

internal interface IGameplaysRepository
{
    Task Add(Gameplay gameplay);
    Task<Gameplay?> Get(GameId gameId);
}