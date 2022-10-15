using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Application.Commands;
using Blef.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Tournaments;

internal sealed class TournamentsController : ModuleControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;

    public TournamentsController(ICommandDispatcher commandDispatcher)
    {
        _commandDispatcher = commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> MakeTournament(CancellationToken cancellation)
    {
        var cmd = new MakeNewTournament();
        var tournament =
            await _commandDispatcher.Dispatch<MakeNewTournament, MakeNewTournament.Result>(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/tournaments/{tournament.TournamentId}", tournament);
    }

    [HttpPost("{tournamentId:Guid}/players")]
    public async Task<IActionResult> JoinTournament(Guid tournamentId, JoinTournamentApi command,
        CancellationToken cancellation)
    {
        var cmd = new JoinTournament(tournamentId, command.PlayerId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/tournaments/{tournamentId}/players/{command.PlayerId}", null);
    }

    [HttpPost("{tournamentId:Guid}/start")]
    public async Task<IActionResult> StartTournament(Guid tournamentId, CancellationToken cancellation)
    {
        var cmd = new StartTournament(tournamentId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/tournaments/{tournamentId}/games/current", null);
    }
}