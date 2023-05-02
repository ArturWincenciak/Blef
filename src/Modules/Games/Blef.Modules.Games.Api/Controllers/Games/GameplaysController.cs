using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games;

internal sealed class GameplaysController : ModuleControllerBase
{
    private const string GAME_ID = "{gameId:Guid}";
    private const string DEALS = "deals";
    private const string DEAL_NUMBER = "{dealNumber:int}";

    private readonly IQueryDispatcher _queryDispatcher;

    public GameplaysController(IQueryDispatcher queryDispatcher) =>
        _queryDispatcher = queryDispatcher;

    [HttpGet($"{GAME_ID}")]
    public async Task<IActionResult> GetGameFlow(Guid gameId, CancellationToken cancellation)
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
}