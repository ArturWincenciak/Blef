using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetLooserHandler : IQueryHandler<GetLooser, GetLooser.Result>
{
    private readonly IGamesRepository _games;

    public GetLooserHandler(IGamesRepository games) =>
        _games = games;

    public Task<GetLooser.Result> Handle(GetLooser query)
    {
        var game = _games.Get(query.GameId);
        var playerId= game.GetLooser();
        return Task.FromResult(new GetLooser.Result(playerId));
    }
}