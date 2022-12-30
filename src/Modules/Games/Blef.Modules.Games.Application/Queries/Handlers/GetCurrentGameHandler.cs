using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetCurrentGameHandler : IQueryHandler<GetCurrentGame, GetCurrentGame.Result>
{
    private readonly ITournamentsRepository _tournaments;

    public GetCurrentGameHandler(ITournamentsRepository tournaments) =>
        _tournaments = tournaments;

    public Task<GetCurrentGame.Result> Handle(GetCurrentGame query, CancellationToken cancellation)
    {
        var tournament = _tournaments.Get(query.TournamentId);
        var game = tournament.GetCurrentGame();
        return Task.FromResult(new GetCurrentGame.Result(game.Id));
    }
}