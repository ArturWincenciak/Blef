using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameIsAlreadyOverException : BlefException
{
    public GameIsAlreadyOverException(Guid gameId)
        : base(
            title: "The game is already over",
            detail: "Cannot make any more moves, the game is over",
            instance: $"/games/{gameId}")
    {
    }
}

//todo: create deal is already over exception