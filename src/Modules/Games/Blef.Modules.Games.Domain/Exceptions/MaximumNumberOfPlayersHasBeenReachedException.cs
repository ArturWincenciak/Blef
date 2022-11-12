using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class MaximumNumberOfPlayersHasBeenReachedException : BlefException
{
    public MaximumNumberOfPlayersHasBeenReachedException(Guid gameId)
        : base(
            title: "The maximum number of players has been reached",
            detail: "No more than 2 players can take part in the game.",
            instance: $"/game/{gameId}")
    {
    }
}