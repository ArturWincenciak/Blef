using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameNotStartedException : BlefException
{
    public GameNotStartedException(GameId gameId) // todo: exception
        : base(title: "TBD", detail: "TBD", instance: "TBD")
    {
    }
}