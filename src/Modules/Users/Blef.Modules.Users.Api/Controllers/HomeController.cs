using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Users.Api.Controllers;

[ApiController]
[Consumes("application/json")]
[Route($"{UsersModule.BASE_PATH}/[controller]")]
internal sealed class HomeController : ControllerBase
{
    [HttpGet]
    [SuppressMessage(category: "Performance", checkId: "CA1822:Mark members as static")]
    public ActionResult<string> Get() =>
        JsonSerializer.Serialize(value: new
        {
            Module = "Users module API",
            RequestTime = DateTime.UtcNow
        }, options: new JsonSerializerOptions {WriteIndented = true});
}