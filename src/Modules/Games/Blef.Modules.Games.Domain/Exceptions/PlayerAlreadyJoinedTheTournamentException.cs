using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class PlayerAlreadyJoinedTheTournamentException : BlefException
{
    public PlayerAlreadyJoinedTheTournamentException(Guid tournamentId, string nick)
        : base(
            title: "Player already joined the tournament",
            detail: $"Player '{nick}' already joined the tournament",
            instance: $"/tournaments/{tournamentId}")
    {
    }
}