using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class CheckHandler : ICommandHandler<Check>
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IGamesRepository _games;

    public CheckHandler(IGamesRepository games, ICommandDispatcher commandDispatcher)
    {
        _games = games;
        _commandDispatcher = commandDispatcher;
    }

    public Task Handle(Check command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        game.Check(command.PlayerId);

        // if finished then start new one.
        var cmd = new StartNextGameInTournament(game.TournamentId);
        _commandDispatcher.Dispatch(cmd, cancellation);
        return Task.CompletedTask;
    }
}