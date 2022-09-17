using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands;
using Blef.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class ErrorHandlingPlaygroundController : GamesControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public ErrorHandlingPlaygroundController(ICommandDispatcher commandDispatcher) =>
        _commandDispatcher = commandDispatcher;

    [HttpPost("cold-framework")]
    public IActionResult PostGuid(
        [Required] Guid guidArg,
        [Required] [Range(3, 6)] int intArg,
        [Required] [MinLength(3)] [MaxLength(5)] string stringArg)
    {
        return Ok(new { guildResult = guidArg, intResult = intArg, stringResult = stringArg });
    }

    [HttpPost("cold-simple-custom")]
    public IActionResult PostCustomType(Custom custom)
    {
        return Ok(custom);
    }

    [HttpPost("cold-nested-custom")]
    public IActionResult PostCustomType(NestedCustom custom)
    {
        return Ok(custom);
    }

    [HttpPost("rise-simple-app-error")]
    public async Task<IActionResult> RiseSimpleAppError()
    {
        await _commandDispatcher.SendAsync(new RiseSimpleAppError());
        return Ok();
    }

    [HttpPost("rise-validation-app-error")]
    public async Task<IActionResult> RiseValidationAppError()
    {
        await _commandDispatcher.SendAsync(new RiseValidationAppError());
        return Ok();
    }

    [HttpPost("rise-internal-server-error")]
    public async Task<IActionResult> RiseInternalServerError()
    {
        await _commandDispatcher.SendAsync(new RiseInternalServerError());
        return Ok();
    }
}

public record Custom(
    [Required] int IntegerType,
    [Required] string StringType,
    [Required] Guid GuidType
);

public record NestedCustom(
    [Required] int IntegerType,
    [Required] string StringType,
    [Required] Guid GuidType,
    [Required] Custom CustomType
);