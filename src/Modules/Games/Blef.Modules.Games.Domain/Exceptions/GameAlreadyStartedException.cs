using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameAlreadyStartedException : BlefException
{
    // todo: ...
    public GameAlreadyStartedException()
        : base(title: "TBD", detail: "TBD", instance: "TODO")
    {
    }
}