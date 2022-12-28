using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class PlayerAlreadyJoinedTheGameException : BlefException
{
    public PlayerAlreadyJoinedTheGameException(Guid gameId, string nick)
        : base(
            title: "Player already joined the game",
            detail: $"Player '{nick}' already joined the game",
            instance: $"/games/{gameId}")
    {
    }
}
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