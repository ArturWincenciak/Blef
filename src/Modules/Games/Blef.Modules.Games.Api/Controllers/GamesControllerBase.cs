using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[ApiController]
[Consumes("application/json")]
[Route($"{GamesModule.BasePath}/[controller]")]
internal abstract class GamesControllerBase : ControllerBase
{
    protected async Task<IActionResult> Ok(Func<Task> funk)
    {
        await funk();
        return Ok();
    }
}