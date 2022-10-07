using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class JoinHandler : ICommandHandler<Join>
{
    private readonly Domain.Games _games;

    public JoinHandler(Domain.Games games)
    {
        _games = games;
    }

    public Task Handle(Join command, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
}