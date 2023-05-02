using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects.Dto;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
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
        return Map(gameFlow);
    }

    private GetGameFlow.Result Map(GameFlowResult gameFlow) =>
        new(gameFlow.Players.Select(Map), gameFlow.Deals.Select(Map));

    private static GetGameFlow.Player Map(GamePlayer player) =>
        new(player.PlayerId.Id, player.Nick.Nick);

    private static GetGameFlow.DealNumber Map(DealId deal) =>
        new(deal.Number.Number);
}