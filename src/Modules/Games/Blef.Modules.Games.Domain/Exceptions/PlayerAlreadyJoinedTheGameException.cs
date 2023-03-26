using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class PlayerAlreadyJoinedTheGameException : BlefException
{
    public PlayerAlreadyJoinedTheGameException(GameId gameId, PlayerNick nick)
        : base(
            title: "Player already joined the game",
            detail: $"Player '{nick.Nick}' already joined the game",
            instance: $"/games/{gameId.Id}")
    {
    }
}