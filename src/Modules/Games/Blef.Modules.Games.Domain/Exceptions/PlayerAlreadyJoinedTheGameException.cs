using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class PlayerAlreadyJoinedTheGameException : BlefException
{
    public PlayerAlreadyJoinedTheGameException(GameId gameId, string nick)
        : base(
            title: "Player already joined the game",
            detail: $"Player '{nick}' already joined the game",
            instance: $"/games/{gameId}")
    {
    }
}