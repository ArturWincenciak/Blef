using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[ApiController]
[Consumes("application/json")]
[Route($"{GamesModule.BASE_PATH}/[controller]")]
internal abstract class ModuleControllerBase : ControllerBase { }