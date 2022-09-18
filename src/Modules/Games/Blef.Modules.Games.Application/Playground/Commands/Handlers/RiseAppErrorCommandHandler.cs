using Blef.Modules.Games.Application.Playground.Exceptions;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Playground.Commands.Handlers;

internal sealed class RiseSimpleAppErrorHandler : ICommandHandler<RiseSimpleAppError>
{
    public Task Handle(RiseSimpleAppError command, CancellationToken cancellation) =>
        throw new PlaygroundBlefAppException();
}