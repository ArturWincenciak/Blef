using Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

public class ErrorPlaygroundService
{
    public void RiseAppError()
    {
        throw new PlaygroundBlefAppException();
    }

    public void RiseInternalServerError()
    {
        throw new ();
    }
}