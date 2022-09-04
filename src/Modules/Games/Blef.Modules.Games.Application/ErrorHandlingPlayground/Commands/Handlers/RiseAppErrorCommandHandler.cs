using Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

public class ErrorPlaygroundService
{
    public void RiseSimpleAppError() =>
        throw new PlaygroundBlefAppException();

    public void RiseValidationAppError() =>
        throw new PlaygroundBlefAppException()
            .WithError(new ExceptionError(
                "prop-1", new[]
                {
                    "contains-number",
                    "is-not-unique"
                }))
            .WithError(new ExceptionError(
                "prop-2", new[]
                {
                    "must-be-scalar"
                }));

    public void RiseInternalServerError() =>
        throw new ();
}