using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Domain.Exceptions;

internal sealed class NotEnoughPlayersException : BlefException
{
    // todo: ...
    public NotEnoughPlayersException()
        : base(title: "TBD", detail: "TBD", instance: "TODO")
    {
    }
}