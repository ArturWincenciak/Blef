using Blef.Modules.Games.Application.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly IGamesRepository _games;

    public CheckHandler(IGamesRepository games) =>
        _games = games;

    public Task Handle(Check command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        game.Check(command.DealNumber, command.PlayerId);
        return Task.CompletedTask;
    }
}