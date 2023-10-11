using System.Diagnostics.CodeAnalysis;
using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Api.Controllers.Games.Queries;
using Blef.Modules.Games.Application.Commands;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games;

[SuppressMessage(category: "ReSharper", checkId: "RouteTemplates.MethodMissingRouteParameters")]
internal sealed partial class GamesController : ModuleControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public GamesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [Tags("Games")]
    [HttpPost]
    public async Task<IActionResult> StartNewGame(CancellationToken cancellation)
    {
        var cmd = new NewGame();
        var game = await _commandDispatcher.Dispatch<NewGame, NewGame.Result>(cmd, cancellation);
        return Created(uri: GetGameFlowQuery.Path(game.GameId), game);
    }

    [Tags("Games")]
    [HttpPost(DealsRoute.ROUTE)]
    public async Task<IActionResult> StartFirstDeal(
        [FromRoute] DealsRoute route,
        CancellationToken cancellation)
    {
        var cmd = new StartFirstDeal(route.GameId);
        var deal = await _commandDispatcher.Dispatch<StartFirstDeal, StartFirstDeal.Result>(cmd, cancellation);
        return Created(uri: GetDealFlowQuery.Path(route.GameId, deal.Number), deal);
    }

    [Tags("Games")]
    [HttpPost(PlayersRoute.ROUTE)]
    public async Task<IActionResult> JoinGame(
        [FromRoute] PlayersRoute route,
        [FromBody] NickPayload payload,
        CancellationToken cancellation)
    {
        var cmd = new JoinGame(route.GameId, payload.Nick);
        var player = await _commandDispatcher.Dispatch<JoinGame, JoinGame.Result>(cmd, cancellation);
        return Created(uri: GetGameFlowQuery.Path(route.GameId), player);
    }

    [Tags("Games")]
    [HttpPost(ChecksRoute.ROUTE)]
    public async Task<IActionResult> Check(
        [FromRoute] ChecksRoute route,
        CancellationToken cancellation)
    {
        var cmd = new Check(route.GameId, route.PlayerId);
        var check = await _commandDispatcher.Dispatch<Check, Check.Result>(cmd, cancellation);
        return Created(uri: GetDealFlowQuery.Path(route.GameId, check.DealNumber), value: null);
    }
}