using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

public class RiseInternalServerErrorHandler : ICommandHandler<RiseInternalServerError>
{
    public Task HandleAsync(RiseInternalServerError command) =>
        throw new ();
}