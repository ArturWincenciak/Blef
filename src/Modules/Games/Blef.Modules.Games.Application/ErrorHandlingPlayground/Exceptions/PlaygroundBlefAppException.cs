using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;

public class PlaygroundBlefAppException : BlefException
{
    public PlaygroundBlefAppException(string message)
        : base(message)
    {
    }
}