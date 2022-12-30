using Blef.Modules.Games.Api.Controllers.Tournaments.Commands;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Tournaments;

internal sealed class TournamentsController : ModuleControllerBase
{
    private const string TOURNAMENT_ID = "{tournamentId:Guid}";
    private const string PLAYERS = "players";
    private const string GAMES_CURRENT = "games/current";

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
        return Created(uri: $"{TournamentUri(tournament.TournamentId)}", tournament);
    }

    [HttpPost($"{TOURNAMENT_ID}/{PLAYERS}")]
    public async Task<IActionResult> JoinTournament(Guid tournamentId, JoinTournamentApi command,
        CancellationToken cancellation)
    {
        var cmd = new JoinTournament(tournamentId, command.Nick);
        var player = await _commandDispatcher.Dispatch<JoinTournament, JoinTournament.Result>(cmd, cancellation);
        return Created(uri: $"{TournamentUri(tournamentId)}/{PLAYERS}/{player.PlayerId}", player);
    }

    [HttpPost($"{TOURNAMENT_ID}/start")]
    public async Task<IActionResult> StartTournament(Guid tournamentId, CancellationToken cancellation)
    {
        var cmd = new StartTournament(tournamentId);
        await _commandDispatcher.Dispatch(cmd, cancellation);
        return Created(uri: $"{TournamentUri(tournamentId)}/{GAMES_CURRENT}", value: null);
    }

    [HttpGet($"{TOURNAMENT_ID}/{GAMES_CURRENT}")]
    public async Task<IActionResult> GetCurrentGame(Guid tournamentId, CancellationToken cancellation)
    {
        var query = new GetCurrentGame(tournamentId);
        var gameId = await _queryDispatcher.Dispatch<GetCurrentGame, GetCurrentGame.Result>(query, cancellation);
        return Ok(gameId);
    }

    private static string TournamentUri(Guid tournamentId) =>
        $"{GamesModule.BASE_PATH}/tournaments/{tournamentId}";
}