using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class JoinGameThatIsAlreadyStartedException : BlefException
{
    public JoinGameThatIsAlreadyStartedException(GameId gameId, string playerNick)
        : base(
            title: "Cannot join game that is already started",
            detail: $"Player '{playerNick}' has tried to join game '{gameId}' that is already started",
            instance: $"/games/{gameId}")
    {
    }
}