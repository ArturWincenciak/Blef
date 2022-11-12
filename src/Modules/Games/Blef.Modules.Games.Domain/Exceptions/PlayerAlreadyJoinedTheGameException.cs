using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class PlayerAlreadyJoinedTheGameException : BlefException
{
    public PlayerAlreadyJoinedTheGameException(Guid gameId, Guid playerId)
        : base(
            title: "Player already joined the game",
            detail: $"Player '{playerId}' already joined the game",
            instance: $"/game/{gameId}")
    {
    }
}

internal sealed class PlayerAlreadyJoinedTheTournamentException : BlefException
{
    public PlayerAlreadyJoinedTheTournamentException(Guid tournamentId, Guid playerId)
        : base(
            title: "Player already joined the tournament",
            detail: $"Player '{playerId}' already joined the tournament",
            instance: $"/tournaments/{tournamentId}")
    {
    }
}