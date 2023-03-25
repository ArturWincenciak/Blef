using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;

    private Guid _conwayPlayerId;

    private GameId _gameId;

    private Guid _grahamPlayerId;

    private Guid _knuthPlayerId;

    private Guid _riemannPlayerId;

    internal BlefClient(HttpClient httpClient) =>
        _httpClient = httpClient;

    internal State GetState() =>
        new(_gameId, _knuthPlayerId, _grahamPlayerId, _riemannPlayerId, _conwayPlayerId);

    async internal Task NewGame() =>
        _gameId = await _httpClient.MakeNewGame();

    async internal Task<GetGameFlow.Result> GetGameFlow() =>
        await _httpClient.GetGameFlow(_gameId);

    async internal Task JoinPlayer(WhichPlayer whichPlayer)
    {
        var player = await _httpClient.JoinPlayer(_gameId, nick: whichPlayer.ToString());
        SetPlayerId(whichPlayer, player);
    }

    async internal Task Deal(WhichPlayer whichPlayer)
    {
        //todo: return localization in header with deal number in path
        await _httpClient.Deal(_gameId, GetPlayerId(whichPlayer));
    }

    async internal Task<GetPlayerCards.Result> GetCards(WhichPlayer whichPlayer, int deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.GetCards(_gameId, deal, playerId);
    }

    async internal Task BidWithSuccess(WhichPlayer whichPlayer, int deal, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidWithSuccess(_gameId, deal, playerId, bid);
    }

    async internal Task<ProblemDetails> BidWithRuleViolation(WhichPlayer whichPlayer, int deal, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidWithRuleViolation(_gameId, deal, playerId, bid);
    }

    async internal Task CheckWithSuccess(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.CheckWithSuccess(_gameId, playerId);
    }

    async internal Task<ProblemDetails> CheckWithRuleViolation(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.CheckWithRuleViolation(_gameId, playerId);
    }

    private Guid GetPlayerId(WhichPlayer whichPlayer) =>
        whichPlayer switch
        {
            WhichPlayer.Knuth => _knuthPlayerId,
            WhichPlayer.Graham => _grahamPlayerId,
            WhichPlayer.Riemann => _riemannPlayerId,
            WhichPlayer.Conway => _conwayPlayerId,
            _ => throw new ArgumentOutOfRangeException(nameof(whichPlayer))
        };

    private void SetPlayerId(WhichPlayer whichPlayer, Guid playerId)
    {
        if (whichPlayer == WhichPlayer.Knuth)
            _knuthPlayerId = playerId;
        else if (whichPlayer == WhichPlayer.Graham)
            _grahamPlayerId = playerId;
        else if (whichPlayer == WhichPlayer.Riemann)
            _riemannPlayerId = playerId;
        else if (whichPlayer == WhichPlayer.Conway)
            _conwayPlayerId = playerId;
        else
            throw new ArgumentOutOfRangeException(nameof(whichPlayer));
    }

    internal record State(
        GameId GameId,
        Guid KnuthPlayerId,
        Guid GrahamPlayerId,
        Guid RiemannPlayerId,
        Guid ConwayPlayerId);
}