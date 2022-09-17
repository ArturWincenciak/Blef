using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;

internal sealed class PlaygroundBlefAppException : BlefException
{
    public PlaygroundBlefAppException()
        : base(
            title: "Playground error",
            detail: "Example Blef application error",
            instance: "/Blef")
    {
    }
}