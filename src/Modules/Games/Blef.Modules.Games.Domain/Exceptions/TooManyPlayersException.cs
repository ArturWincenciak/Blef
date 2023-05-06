using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class TooManyPlayersException : BlefException
{
    public TooManyPlayersException(GameId gameId)
        : base(
            title: "The maximum number of game players has been reached",
            detail: "No more than four players can take part in the game",
            instance: $"/games/{gameId}")
    {
    }
}