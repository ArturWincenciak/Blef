using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly IGamesRepository _games;

    public CheckHandler(IGamesRepository games) =>
        _games = games;

    public Task Handle(Check command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        game.Check(command.PlayerId);
        return Task.CompletedTask;
    }
}