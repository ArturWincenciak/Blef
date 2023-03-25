using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class NoBidToCheckException : BlefException
{
    public NoBidToCheckException(GameId gameId)
        : base(
            title: "There is no bid to check it",
            detail: "Game must be started by at least one bid, cannot check when the game has not started",
            instance: $"/games/{gameId}")
    {
    }
}