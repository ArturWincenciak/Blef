using Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands;
using Blef.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class CommandHandlingPlaygroundController : GamesControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public CommandHandlingPlaygroundController(ICommandDispatcher commandDispatcher) =>
        _commandDispatcher = commandDispatcher;

    [HttpPost]
    public async Task<IActionResult> Post(JustCommand command) =>
        await Ok(() => _commandDispatcher.SendAsync(command));
}