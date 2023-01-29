using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class MinimumGamePlayersNotReachedException : BlefException
{
    public MinimumGamePlayersNotReachedException(Guid gameId)
        : base(
            title: "The minimum number of game players has not been reached",
            detail: "Minimum number of game players must be at least two",
            instance: $"/games/{gameId}")
    {
    }
}