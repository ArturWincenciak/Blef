using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal class PlayerAlreadyLostTheGameException : BlefException
{
    public PlayerAlreadyLostTheGameException(GameId gameId, PlayerId playerId)
        : base(
            title: "Player already lost the game",
            detail: $"Player '{playerId}' already lost the game '{gameId}'")
    {
    }
}