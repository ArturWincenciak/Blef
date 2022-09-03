using Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

public class RiseAppErrorCommandHandler
{
    public void Handle()
    {
        throw new PlaygroundBlefAppException("Error handling playground.");
    }
}