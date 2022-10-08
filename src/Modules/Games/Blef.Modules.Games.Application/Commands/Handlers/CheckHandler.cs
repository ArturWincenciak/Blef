using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly Domain.Games _games;

    public CheckHandler(Domain.Games games) =>
        _games = games;

    public Task Handle(Check command, CancellationToken cancellation)
    {
        var game = _games.GetExistingGame(command.GameId);
        game.Check(command.PlayerId);
        return Task.CompletedTask;
    }
}