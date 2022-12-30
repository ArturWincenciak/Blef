using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class MakeNewTournamentHandler : ICommandHandler<MakeNewTournament, MakeNewTournament.Result>
{
    private readonly ITournamentsRepository _tournaments;

    public MakeNewTournamentHandler(ITournamentsRepository tournaments) =>
        _tournaments = tournaments;

    public async Task<MakeNewTournament.Result> Handle(MakeNewTournament command, CancellationToken cancellation)
    {
        var tournament = Tournament.Create();
        _tournaments.Add(tournament);
        var result = new MakeNewTournament.Result(tournament.Id);
        return await Task.FromResult(result);
    }
}