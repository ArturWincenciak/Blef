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
        var command = new MakeNewGame();
        var game = await _commandDispatcher.Dispatch<MakeNewGame, MakeNewGame.Result>(command, cancellation);
        return Created($"{GamesModule.BasePath}/games/{game.GameId}", game);
    }
}