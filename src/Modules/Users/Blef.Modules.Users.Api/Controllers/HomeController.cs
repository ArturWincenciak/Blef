using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Users.Api.Controllers;

[Route(UsersModule.BasePath)]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() =>
        JsonSerializer.Serialize(new
        {
            Module = "Users module API",
            RequestTime = DateTime.UtcNow
        }, new JsonSerializerOptions {WriteIndented = true});
}