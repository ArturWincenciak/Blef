using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class StartTournamentHandler : ICommandHandler<StartTournament>
{
    private readonly ITournamentsRepository _tournaments;

    public StartTournamentHandler (ITournamentsRepository tournaments) =>
        _tournaments = tournaments;

    public Task Handle(StartTournament command, CancellationToken cancellation)
    {
        var tournament = _tournaments.Get(command.TournamentId);
        tournament.Start();
        return Task.CompletedTask;
    }
}