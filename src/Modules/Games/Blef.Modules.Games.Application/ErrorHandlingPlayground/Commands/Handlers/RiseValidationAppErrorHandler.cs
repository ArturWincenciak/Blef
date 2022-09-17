using Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Kernel.Exceptions;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

internal class RiseValidationAppErrorHandler : ICommandHandler<RiseValidationAppError>
{
    public Task HandleAsync(RiseValidationAppError command) =>
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
}