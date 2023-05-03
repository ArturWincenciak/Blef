using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class EmptyPlayerNickException : BlefException
{
    public EmptyPlayerNickException(GameId gameId) // todo: exception
        : base(title: "TBD", detail: "TBD", instance: "TBD")
    {
    }
}