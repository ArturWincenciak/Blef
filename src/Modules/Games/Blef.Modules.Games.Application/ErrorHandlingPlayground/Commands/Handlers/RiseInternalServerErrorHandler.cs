using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

internal sealed class RiseInternalServerErrorHandler : ICommandHandler<RiseInternalServerError>
{
    public Task Handle(RiseInternalServerError command, CancellationToken cancellation) =>
        throw new ();
}