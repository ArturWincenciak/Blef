using Blef.Modules.Games.Api.Controllers.Tournaments.Commands;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Tournaments;

internal sealed class TournamentsController : ModuleControllerBase
{
    private readonly ICommandDispatcher _commandDispatcher;
    private readonly IQueryDispatcher _queryDispatcher;

    public TournamentsController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
    {
        _commandDispatcher = commandDispatcher;
        _queryDispatcher = queryDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> MakeTournament(CancellationToken cancellation)
    {
        var cmd = new MakeNewTournament();
        var tournament = await _commandDispatcher
            .Dispatch<MakeNewTournament, MakeNewTournament.Result>(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/tournaments/{tournament.TournamentId}", tournament);
    }

    [HttpPost("{tournamentId:Guid}/players")]
    public async Task<IActionResult> JoinTournament(Guid tournamentId, JoinTournamentApi command,
        CancellationToken cancellation)
    {
        var cmd = new JoinTournament(tournamentId, command.Nick);
        var player = await _commandDispatcher.Dispatch<JoinTournament, JoinTournament.Result>(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/tournaments/{tournamentId}/players/{player.PlayerId}", player);
    }

    [HttpPost("{tournamentId:Guid}/start")]
    public async Task<IActionResult> StartTournament(Guid tournamentId, CancellationToken cancellation)
    {
        var cmd = new StartTournament(tournamentId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created($"{GamesModule.BasePath}/tournaments/{tournamentId}/games/current", null);
    }

    [HttpGet("{tournamentId:Guid}/games/current")]
    public async Task<IActionResult> GetCurrentGame(Guid tournamentId, CancellationToken cancellation)
    {
        var query = new GetCurrentGame(tournamentId);
        var gameId = await _queryDispatcher.Dispatch<GetCurrentGame, GetCurrentGame.Result>(query, cancellation);
        return Ok(gameId);
    }
}