using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal class PlayerNotJoinedTheGameException : BlefException
{
    public PlayerNotJoinedTheGameException(GameId gameId, PlayerId playerId)
        : base(
            title: "Player not joined the game",
            detail: $"Player '{playerId}' not joined the game '{gameId}'")
    {
    }
}