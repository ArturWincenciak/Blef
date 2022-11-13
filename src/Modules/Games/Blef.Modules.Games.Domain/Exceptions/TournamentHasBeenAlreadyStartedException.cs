using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class TournamentHasBeenAlreadyStartedException : BlefException
{
    public TournamentHasBeenAlreadyStartedException(Guid tournamentId)
        : base(
            title: "Tournament has been already started",
            detail: "Tournament cannot be started once again",
            instance: $"/tournaments/{tournamentId}")
    {
    }
}