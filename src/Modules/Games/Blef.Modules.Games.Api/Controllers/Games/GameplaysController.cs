using System.Diagnostics.CodeAnalysis;
using Blef.Modules.Games.Api.Controllers.Games.Queries;
using Blef.Modules.Games.Application.Queries;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games;

[SuppressMessage(category: "ReSharper", checkId: "RouteTemplates.MethodMissingRouteParameters")]
internal sealed class GameplaysController : ModuleControllerBase
{
    private readonly IQueryDispatcher _queryDispatcher;

    public GameplaysController(IQueryDispatcher queryDispatcher) =>
        _queryDispatcher = queryDispatcher;


    [HttpGet(GetGameFlowQuery.ROUTE)]
    public async Task<IActionResult> GetGameFlow(
        [FromRoute] GetGameFlowQuery route,
        CancellationToken cancellation)
    {
        var query = new GetGame(route.GameId);
        var gameFlow = await _queryDispatcher.Dispatch<GetGame, GetGame.Result>(query, cancellation);
        return Ok(gameFlow);
    }

    [HttpGet(GetDealFlowQuery.ROUTE)]
    public async Task<IActionResult> GetDealFlow(
        [FromRoute] GetDealFlowQuery route,
        CancellationToken cancellation)
    {
        var query = new GetDeal(route.GameId, route.DealNumber);
        var dealFlow = await _queryDispatcher.Dispatch<GetDeal, GetDeal.Result>(query, cancellation);
        return Ok(dealFlow);
    }
}