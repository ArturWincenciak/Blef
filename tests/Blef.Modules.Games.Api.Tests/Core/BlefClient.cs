using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using GameId = Blef.Modules.Games.Api.Tests.Core.ValueObjects.GameId;
using PlayerId = Blef.Modules.Games.Api.Tests.Core.ValueObjects.PlayerId;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;

    private GameId _gameId;

    private PlayerId _conwayPlayerId;
    private PlayerId _grahamPlayerId;
    private PlayerId _knuthPlayerId;
    private PlayerId _riemannPlayerId;

    internal BlefClient(HttpClient httpClient) =>
        _httpClient = httpClient;

    internal State GetState() =>
        new(_gameId, _knuthPlayerId, _grahamPlayerId, _riemannPlayerId, _conwayPlayerId);

    async internal Task NewGame() =>
        _gameId = await _httpClient.NewGame();

    async internal Task<GetDealFlow.Result> GetDealFlow(DealNumber dealNumber) =>
        await _httpClient.GetDealFlow(_gameId, dealNumber);

    async internal Task JoinPlayer(WhichPlayer whichPlayer)
    {
        var player = await _httpClient.JoinPlayer(_gameId, nick: whichPlayer.ToString());
        SetPlayerId(whichPlayer, player);
    }

    async internal Task Deal(WhichPlayer whichPlayer)
    {
        //todo: return localization in header with deal number in path
        await _httpClient.NewDeal(_gameId, GetPlayerId(whichPlayer));
    }

    async internal Task<GetPlayerCards.Result> GetCards(WhichPlayer whichPlayer, Deal deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.GetCards(_gameId, deal, playerId);
    }

    async internal Task BidWithSuccess(WhichPlayer whichPlayer, Deal deal, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidWithSuccess(_gameId, deal, playerId, bid);
    }

    async internal Task<ProblemDetails> BidWithRuleViolation(WhichPlayer whichPlayer, Deal deal, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidWithRuleViolation(_gameId, deal, playerId, bid);
    }

    async internal Task CheckWithSuccess(WhichPlayer whichPlayer, Deal deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.CheckWithSuccess(_gameId, deal, playerId);
    }

    async internal Task<ProblemDetails> CheckWithRuleViolation(WhichPlayer whichPlayer, Deal deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.CheckWithRuleViolation(_gameId, deal, playerId);
    }

    private PlayerId GetPlayerId(WhichPlayer whichPlayer) =>
        whichPlayer switch
        {
            WhichPlayer.Knuth => _knuthPlayerId,
            WhichPlayer.Graham => _grahamPlayerId,
            WhichPlayer.Riemann => _riemannPlayerId,
            WhichPlayer.Conway => _conwayPlayerId,
            _ => throw new ArgumentOutOfRangeException(nameof(whichPlayer))
        };

    private void SetPlayerId(WhichPlayer whichPlayer, PlayerId playerId)
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
        PlayerId KnuthPlayerId,
        PlayerId GrahamPlayerId,
        PlayerId RiemannPlayerId,
        PlayerId ConwayPlayerId);
}