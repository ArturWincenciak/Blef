using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;

internal sealed class JustCommandHandler : ICommandHandler<JustCommand>
{
    public Task HandleAsync(JustCommand command) =>
        Task.CompletedTask;
}