using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGameFlow, GetGameFlow.Result>
{
    private readonly IGamesRepository _games;

    public GetGameHandler(IGamesRepository games) =>
        _games = games;

    public async Task<GetGameFlow.Result> Handle(GetGameFlow query, CancellationToken cancellation)
    {
        var game = _games.Get(query.GameId);
        var gameFlow = game.GetGameFlow();
        return null;
    }
}