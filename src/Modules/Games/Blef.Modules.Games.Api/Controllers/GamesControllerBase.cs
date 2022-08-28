using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[ApiController]
[Route($"{GamesModule.BasePath}/[controller]")]
internal abstract class GamesControllerBase : ControllerBase
{

}