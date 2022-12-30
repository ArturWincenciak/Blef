using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class JoinTournamentHandler : ICommandHandler<JoinTournament, JoinTournament.Result>
{
    private readonly ITournamentsRepository _tournaments;

    public JoinTournamentHandler(ITournamentsRepository tournaments) =>
        _tournaments = tournaments;

    public Task<JoinTournament.Result> Handle(JoinTournament command, CancellationToken cancellation)
    {
        var tournament = _tournaments.Get(command.TournamentId);
        var player = tournament.Join(command.Nick);
        return Task.FromResult(new JoinTournament.Result(player.PlayerId, command.Nick));
    }
}