using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class MaximumNumberOfGamePlayersHasBeenReachedException : BlefException
{
    public MaximumNumberOfGamePlayersHasBeenReachedException(Guid gameId)
        : base(
            title: "The maximum number of game players has been reached",
            detail: "No more than 2 players can take part in the game",
            instance: $"/game/{gameId}")
    {
    }
}

internal sealed class MaximumNumberOfTournamentPlayersHasBeenReachedException : BlefException
{
    public MaximumNumberOfTournamentPlayersHasBeenReachedException(Guid tournamentId)
        : base(
            title: "The maximum number of tournament players has been reached",
            detail: "No more than 2 players can take part in the tournament",
            instance: $"/tournaments/{tournamentId}")
    {
    }
}
