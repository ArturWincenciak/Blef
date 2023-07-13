using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameAlreadyStartedException : BlefException
{
    public GameAlreadyStartedException(GameId gameId)
        : base(
            title: "Game already started",
            detail: $"The game '{gameId}' has already started")
    {
    }
}