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
        [Required] [MinLength(3)] [MaxLength(5)] string stringArg) =>
        Ok(new { guildResult = guidArg, intResult = intArg, stringResult = stringArg });

    [HttpPost("cold-simple-custom")]
    public IActionResult PostCustomType(Custom custom) =>
        Ok(custom);

    [HttpPost("cold-nested-custom")]
    public IActionResult PostCustomType(NestedCustom custom) =>
        Ok(custom);

    [HttpPost("rise-simple-app-error")]
    public async Task<IActionResult> RiseSimpleAppError(CancellationToken cancellation) =>
        await Ok(() => _commandDispatcher.Dispatch(new RiseSimpleAppError(), cancellation));

    [HttpPost("rise-validation-app-error")]
    public async Task<IActionResult> RiseValidationAppError(CancellationToken cancellation) =>
        await Ok(() => _commandDispatcher.Dispatch(new RiseValidationAppError(), cancellation));

    [HttpPost("rise-internal-server-error")]
    public async Task<IActionResult> RiseInternalServerError(CancellationToken cancellation) =>
        await Ok(() => _commandDispatcher.Dispatch(new RiseInternalServerError(), cancellation));
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