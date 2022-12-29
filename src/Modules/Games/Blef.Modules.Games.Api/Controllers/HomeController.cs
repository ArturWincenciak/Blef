using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[Route(GamesModule.BASE_PATH)]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() =>
        JsonSerializer.Serialize(value: new
        {
            Module = "Games module API",
            RequestTime = DateTime.UtcNow
        }, options: new JsonSerializerOptions {WriteIndented = true});
}