using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class JoinGameAlreadyStartedException : BlefException
{
    public JoinGameAlreadyStartedException(GameId gameId, PlayerNick playerNick)
        : base(
            title: "Cannot join game that is already started",
            detail: $"Player '{playerNick.Nick}' has tried to join game '{gameId.Id}' that is already started",
            instance: $"/games/{gameId.Id}")
    {
    }
}