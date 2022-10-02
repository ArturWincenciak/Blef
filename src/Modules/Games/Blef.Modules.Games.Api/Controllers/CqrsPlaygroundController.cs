using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Application.Playground.Commands;
using Blef.Modules.Games.Application.Playground.Queries;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers;

internal sealed class CqrsPlaygroundController : GamesControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public CqrsPlaygroundController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost("just")]
    public async Task<IActionResult> Post(JustCommand command, CancellationToken cancellation) =>
        await Ok(() => _commandDispatcher.Dispatch(command, cancellation));

    [HttpPost("effect")]
    public async Task<IActionResult> Post(EffectCommand command, CancellationToken cancellation) =>
        Ok(await _commandDispatcher.Dispatch<EffectCommand, EffectCommand.Result>(command, cancellation));

    [HttpGet("just")]
    public async Task<IActionResult> Get([Range(1, int.MaxValue)] int id, CancellationToken cancellation) =>
        Ok(await _queryDispatcher.Dispatch<JustQuery, JustQuery.Result>(new JustQuery(id), cancellation));
}