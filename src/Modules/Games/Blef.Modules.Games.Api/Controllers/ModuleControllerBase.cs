using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

[ApiController]
[Consumes("application/json")]
[Route($"{GamesModule.BasePath}/[controller]")]
internal abstract class ModuleControllerBase : ControllerBase { }