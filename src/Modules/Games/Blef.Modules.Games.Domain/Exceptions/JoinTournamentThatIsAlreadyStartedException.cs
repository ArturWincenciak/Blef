using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class JoinTournamentThatIsAlreadyStartedException : BlefException
{
    public JoinTournamentThatIsAlreadyStartedException(Guid tournamentsId, string nick)
        : base(
            title: "Cannot join tournament that is already started",
            detail: $"Player '{nick}' has tried to join tournament '{tournamentsId}' that is already started",
            instance: $"/tournaments/{tournamentsId}")
    {
    }
}