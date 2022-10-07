using Blef.Modules.Games.Application.Commands;
using Blef.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class GamesController : GamesControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public GamesController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> MakeGame(CancellationToken cancellation)
    {
        var cmd = new MakeNewGame();
        var game = await _commandDispatcher.Dispatch<MakeNewGame, MakeNewGame.Result>(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/games/{game.GameId}", game);
    }

    public record JoinGameApi(Guid PlayerId);
    [HttpPost("{gameId:Guid}/players")]
    public async Task<IActionResult> JoinGame(Guid gameId, JoinGameApi command, CancellationToken cancellation)
    {
        var cmd = new Join(gameId, command.PlayerId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/games/{gameId}/players/{command.PlayerId}", null);
    }
}