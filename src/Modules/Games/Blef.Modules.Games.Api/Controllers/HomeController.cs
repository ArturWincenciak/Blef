using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[Route("games-module")]
internal class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() =>
        JsonSerializer.Serialize(new
        {
            Module = "Games module API",
            RequestTime = DateTime.UtcNow
        }, new JsonSerializerOptions {WriteIndented = true});
}