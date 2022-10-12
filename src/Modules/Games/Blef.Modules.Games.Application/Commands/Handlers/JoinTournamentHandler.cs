using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;

namespace Blef.Modules.Games.Application.Commands.Handlers;

internal sealed class JoinTournamentHandler : ICommandHandler<JoinTournament>
{
    private readonly ITournamentsRepository _tournaments;

    public JoinTournamentHandler (ITournamentsRepository tournaments) =>
        _tournaments = tournaments;

    public Task Handle(JoinTournament command, CancellationToken cancellation)
    {
        var tournament = _tournaments.Get(command.TournamentId);
        tournament.Join(command.PlayerId);
        return Task.CompletedTask;
    }
}