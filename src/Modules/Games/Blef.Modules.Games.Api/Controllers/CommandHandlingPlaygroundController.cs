using Blef.Modules.Games.Application.Playground.Commands;
using Blef.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class CommandHandlingPlaygroundController : GamesControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CommandHandlingPlaygroundController(ICommandDispatcher commandDispatcher) =>
        _commandDispatcher = commandDispatcher;

    [HttpPost("just")]
    public async Task<IActionResult> Post(JustCommand command, CancellationToken cancellation) =>
        await Ok(() => _commandDispatcher.Dispatch(command, cancellation));

    [HttpPost("effect")]
    public async Task<IActionResult> Post(EffectCommand command, CancellationToken cancellation) =>
        Ok(await _commandDispatcher.Dispatch<EffectCommand, EffectCommand.Result>(command, cancellation));
}