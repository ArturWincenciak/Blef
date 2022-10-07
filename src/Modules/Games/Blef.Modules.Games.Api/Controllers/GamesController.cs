using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[Route(GamesModule.BasePath)]
internal sealed class GamesController : GamesControllerBase
{
    private static readonly Blef.Modules.Games.Application.Games _games = new();

    [HttpPost]
    public ActionResult<string> CreateGame() =>
        JsonSerializer.Serialize(new
        {
            gameId = _games.CreateGame()
        }, new JsonSerializerOptions { WriteIndented = true });
}