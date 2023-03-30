using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games;

internal sealed class GamesController : ModuleControllerBase
{
    private const string GAME_ID = "{gameId:Guid}";
    private const string PLAYER_ID = "{playerId:Guid}";
    private const string DEAL_NUMBER = "{dealNumber:int}";
    private const string GAMES = "games";
    private const string DEALS = "deals";
    private const string PLAYERS = "players";
    private const string CARDS = "cards";
    private const string BIDS = "bids";
    private const string CHECKS = "checks";

    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public GamesController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> NewGame(CancellationToken cancellation)
    {
        var cmd = new NewGame();
        var game = await _commandDispatcher.Dispatch<NewGame, NewGame.Result>(cmd, cancellation);
        return Created(uri: $"{GameUri(game.GameId)}", game);
    }

    [HttpGet($"{GAME_ID}")]
    public async Task<IActionResult> GetDealFlow(Guid gameId, CancellationToken cancellation)
    {
        var query = new GetGameFlow(new GameId(gameId));
        var gameFlow = await _queryDispatcher.Dispatch<GetGameFlow, GetGameFlow.Result>(query, cancellation);
        return Ok(gameFlow);
    }

    [HttpGet($"{GAME_ID}/{DEALS}/{DEAL_NUMBER}")]
    public async Task<IActionResult> GetDealFlow(Guid gameId, int dealNumber, CancellationToken cancellation)
    {
        var query = new GetDealFlow(GameId: new GameId(gameId), DealNumber: new DealNumber(dealNumber));
        var dealFlow = await _queryDispatcher.Dispatch<GetDealFlow, GetDealFlow.Result>(query, cancellation);
        return Ok(dealFlow);
    }

    [HttpPost($"{GAME_ID}/{PLAYERS}")]
    public async Task<IActionResult> JoinGame(Guid gameId, JoinGameApi command, CancellationToken cancellation)
    {
        var cmd = new JoinGame(GameId: new GameId(gameId), command.Nick);
        var player = await _commandDispatcher.Dispatch<JoinGame, JoinGame.Result>(cmd, cancellation);
        return Created(uri: $"{PlayerUri(gameId, player.PlayerId)}", player);
    }

    [HttpPost($"{GAME_ID}/{PLAYERS}/{PLAYER_ID}/{DEALS}")]
    public async Task<IActionResult> NewDeal(Guid gameId, CancellationToken cancellation)
    {
        var cmd = new NewDeal(new GameId(gameId));
        var deal = await _commandDispatcher.Dispatch<NewDeal, NewDeal.Result>(cmd, cancellation);
        return Created(uri: $"{GameUri(gameId)}/{DEALS}/{deal.Number}", deal);
    }

    [HttpGet($"{GAME_ID}/{PLAYERS}/{PLAYER_ID}/{DEALS}/{DEAL_NUMBER}/{CARDS}")]
    public async Task<IActionResult> GetCards(Guid gameId, Guid playerId, int dealNumber,
        CancellationToken cancellation)
    {
        var query = new GetPlayerCards(GameId: new GameId(gameId), PlayerId: new PlayerId(playerId),
            DealNumber: new DealNumber(dealNumber));
        var cards = await _queryDispatcher.Dispatch<GetPlayerCards, GetPlayerCards.Result>(query, cancellation);
        return Ok(cards);
    }

    [HttpPost($"{GAME_ID}/{PLAYERS}/{PLAYER_ID}/{DEALS}/{DEAL_NUMBER}/{BIDS}")]
    public async Task<IActionResult> Bid(Guid gameId, Guid playerId, int dealNumber, BidApi command,
        CancellationToken cancellation)
    {
        var cmd = new Bid(GameId: new GameId(gameId), PlayerId: new PlayerId(playerId),
            DealNumber: new DealNumber(dealNumber), command.PokerHand);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created(uri: $"{PlayerUri(gameId, playerId)}/{BIDS}", value: null);
    }

    [HttpPost($"{GAME_ID}/{PLAYERS}/{PLAYER_ID}/{DEALS}/{DEAL_NUMBER}/{CHECKS}")]
    public async Task<IActionResult> CheckBid(Guid gameId, Guid playerId, int dealNumber,
        CancellationToken cancellation)
    {
        var cmd = new Check(GameId: new GameId(gameId), PlayerId: new PlayerId(playerId),
            DealNumber: new DealNumber(dealNumber));
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created(uri: $"{PlayerUri(gameId, playerId)}/checks", value: null);
    }

    private static string PlayerUri(Guid gameId, Guid playerId) =>
        $"{GameUri(gameId)}/{PLAYERS}/{playerId}";

    private static string GameUri(Guid gameId) =>
        $"{GamesModule.BASE_PATH}/{GAMES}/{gameId}";
}