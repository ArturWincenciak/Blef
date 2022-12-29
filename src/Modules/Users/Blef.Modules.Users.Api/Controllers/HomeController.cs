using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Users.Api.Controllers;

[Route(UsersModule.BASE_PATH)]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    public ActionResult<string> Get() =>
        JsonSerializer.Serialize(value: new
        {
            Module = "Users module API",
            RequestTime = DateTime.UtcNow
        }, options: new JsonSerializerOptions {WriteIndented = true});
}