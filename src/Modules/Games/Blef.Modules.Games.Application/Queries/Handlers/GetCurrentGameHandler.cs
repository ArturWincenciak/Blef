using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetCurrentGameHandler : IQueryHandler<GetCurrentGame, GetCurrentGame.Result>
{
    private readonly ITournamentsRepository _tournaments;

    public GetCurrentGameHandler(ITournamentsRepository tournaments)
    {
        _tournaments = tournaments;
    }

    public Task<GetCurrentGame.Result> Handle(GetCurrentGame query)
    {
        var tournament = _tournaments.Get(query.TournamentId);
        var game = tournament.GetCurrentGame();
        return Task.FromResult(new GetCurrentGame.Result(game.Id));
    }
}