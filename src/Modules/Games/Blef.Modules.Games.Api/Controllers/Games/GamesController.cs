using Blef.Modules.Games.Api.Controllers.Games.Commands;
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
    public async Task<IActionResult> MakeGame(CancellationToken cancellation)
    {
        var cmd = new MakeNewGame();
        var game = await _commandDispatcher.Dispatch<MakeNewGame, MakeNewGame.Result>(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/games/{game.GameId}", game);
    }

    [HttpPost("{gameId:Guid}/players")]
    public async Task<IActionResult> JoinGame(Guid gameId, JoinGameApi command, CancellationToken cancellation)
    {
        var cmd = new JoinGame(gameId, command.Nick);
        var player = await _commandDispatcher.Dispatch<JoinGame, JoinGame.Result>(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/games/{gameId}/players/{player.PlayerId}", player);
    }

    [HttpGet("{gameId:Guid}/players/{playerId:Guid}/cards")]
    public async Task<IActionResult> GetCards(Guid gameId, Guid playerId, CancellationToken cancellation)
    {
        var query = new GetPlayerCards(gameId, playerId);
        var cards = await _queryDispatcher.Dispatch<GetPlayerCards, GetPlayerCards.Result>(query, cancellation);
        return Ok(cards);
    }

    [HttpPost("{gameId:Guid}/players/{playerId:Guid}/bids")]
    public async Task<IActionResult> Bid(Guid gameId, Guid playerId, BidApi command, CancellationToken cancellation)
    {
        var cmd = new Bid(gameId, playerId, command.PokerHand);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/games/{gameId}/players/{playerId}/bids", null);
    }

    [HttpPost("{gameId:Guid}/players/{playerId:Guid}/checks")]
    public async Task<IActionResult> CheckBid(Guid gameId, Guid playerId, CancellationToken cancellation)
    {
        var cmd = new Check(gameId, playerId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/games/{gameId}/players/{playerId}/checks", null);
    }

    [HttpGet("{gameId:Guid}/looser")]
    public async Task<IActionResult> GetLooser(Guid gameId, CancellationToken cancellation)
    {
        var query = new GetLooser(gameId);
        var looser = await _queryDispatcher.Dispatch<GetLooser, GetLooser.Result>(query, cancellation);
        return Ok(looser);
    }
}