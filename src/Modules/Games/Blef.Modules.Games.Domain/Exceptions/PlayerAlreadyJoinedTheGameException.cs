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