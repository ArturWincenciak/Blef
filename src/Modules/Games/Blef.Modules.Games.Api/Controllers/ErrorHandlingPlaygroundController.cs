using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class ErrorHandlingPlaygroundController : GamesControllerBase
{
    [HttpPost("framework")]
    public IActionResult PostGuid(
        [Required] Guid guidArg,
        [Required] [Range(3, 6)] int intArg,
        [Required] [MinLength(3)] [MaxLength(5)] string stringArg)
    {
        return Ok(new { guildResult = guidArg, intResult = intArg, stringResult = stringArg });
    }

    [HttpPost("simple-custom")]
    public IActionResult PostCustomType(Custom custom)
    {
        return Ok(custom);
    }

    [HttpPost("nested-custom")]
    public IActionResult PostCustomType(NestedCustom custom)
    {
        return Ok(custom);
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