using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameNotStartedException : BlefException
{
    public GameNotStartedException(GameId gameId)
        : base(
            title: "Game not started",
            detail: $"The game {gameId} has not yet started")
    {
    }
}