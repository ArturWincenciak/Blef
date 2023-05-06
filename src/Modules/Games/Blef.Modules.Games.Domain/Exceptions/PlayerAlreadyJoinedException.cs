using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class PlayerAlreadyJoinedException : BlefException
{
    public PlayerAlreadyJoinedException(GameId gameId, PlayerNick nick)
        : base(
            title: "Player already joined the game",
            detail: $"Player '{nick.Nick}' already joined the game",
            instance: $"/games/{gameId.Id}")
    {
    }
}