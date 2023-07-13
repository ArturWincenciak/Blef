using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameOverException : BlefException
{
    public GameOverException(GameId gameId)
        : base(
            title: "The game is already over",
            detail: "Cannot make any more moves, the game is over")
    {
    }
}