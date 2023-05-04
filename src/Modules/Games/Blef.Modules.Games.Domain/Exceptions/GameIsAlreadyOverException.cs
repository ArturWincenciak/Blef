using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

// todo: use them
internal sealed class GameIsAlreadyOverException : BlefException
{
    public GameIsAlreadyOverException(GameId gameId)
        : base(
            title: "The game is already over",
            detail: "Cannot make any more moves, the game is over",
            instance: $"/games/{gameId}")
    {
    }
}