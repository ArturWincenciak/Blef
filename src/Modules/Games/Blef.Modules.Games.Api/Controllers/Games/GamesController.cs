using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Api.Controllers.Games.Queries;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games;

internal sealed class GamesController : ModuleControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public GamesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> StartNewGame(CancellationToken cancellation)
    {
        var cmd = new NewGame();
        var game = await _commandDispatcher.Dispatch<NewGame, NewGame.Result>(cmd, cancellation);
        return Created(uri: GetGameFlowQuery.Path(game.GameId), game); // todo: test the location header
    }

    [HttpPost(DealsRoute.ROUTE)]
    public async Task<IActionResult> StartFirstDeal(
        [FromRoute] DealsRoute route,
        CancellationToken cancellation)
    {
        var cmd = new StartFirstDeal(route.GameId);
        var deal = await _commandDispatcher.Dispatch<StartFirstDeal, StartFirstDeal.Result>(cmd, cancellation);
        return Created(uri: GetDealFlowQuery.Path(route.GameId, deal.Number), deal);
    }

    [HttpPost(PlayersRoute.ROUTE)]
    public async Task<IActionResult> JoinGame(
        [FromRoute] PlayersRoute route,
        [FromBody] NickDto dto,
        CancellationToken cancellation)
    {
        var cmd = new JoinGame(route.GameId, dto.Nick);
        var player = await _commandDispatcher.Dispatch<JoinGame, JoinGame.Result>(cmd, cancellation);
        return Created(uri: GetGameFlowQuery.Path(route.GameId), player);
    }

    [HttpPost(BidsRoute.ROUTE)]
    public async Task<IActionResult> Bid(
        [FromRoute] BidsRoute route,
        [FromBody] BidDto dto,
        CancellationToken cancellation)
    {
        var cmd = new Bid(route.GameId, route.PlayerId, dto.PokerHand);
        var bid = await _commandDispatcher.Dispatch<Bid, Bid.Result>(cmd, cancellation);
        return Created(uri: GetDealFlowQuery.Path(route.GameId, bid.DealNumber), value: null);
    }

    [HttpPost(ChecksRoute.ROUTE)]
    public async Task<IActionResult> Check(
        [FromRoute] ChecksRoute route,
        CancellationToken cancellation)
    {
        var cmd = new Check(route.GameId, route.PlayerId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created(uri: "todo", value: null); // todo: location header, test the header
    }

    [HttpGet(GetCardsQuery.ROUTE)]
    public async Task<IActionResult> GetCards(
        [FromRoute] GetCardsQuery route,
        CancellationToken cancellation)
    {
        var query = new GetPlayerCards(route.GameId, route.PlayerId, route.DealNumber);
        var cards = await _queryDispatcher.Dispatch<GetPlayerCards, GetPlayerCards.Result>(query, cancellation);
        return Ok(cards);
    }
}
