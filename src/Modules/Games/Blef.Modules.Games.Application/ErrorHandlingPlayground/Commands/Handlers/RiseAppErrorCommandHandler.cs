using Blef.Modules.Games.Application.ErrorHandlingPlayground.Exceptions;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

internal sealed class RiseSimpleAppErrorHandler : ICommandHandler<RiseSimpleAppError>
{
    public Task HandleAsync(RiseSimpleAppError command) =>
        throw new PlaygroundBlefAppException();
}