using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class GameAlreadyStartedException : BlefException
{
    public GameAlreadyStartedException() // todo: exception
        : base(title: "TBD", detail: "TBD", instance: "TODO")
    {
    }
}