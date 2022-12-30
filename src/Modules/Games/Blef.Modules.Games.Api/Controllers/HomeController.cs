using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class HomeController : ModuleControllerBase
{
    [HttpGet]
    [SuppressMessage(category: "Performance", checkId: "CA1822:Mark members as static")]
    public ActionResult<string> Get() =>
        JsonSerializer.Serialize(value: new
        {
            Module = "Games module API",
            RequestTime = DateTime.UtcNow
        }, options: new JsonSerializerOptions {WriteIndented = true});
}