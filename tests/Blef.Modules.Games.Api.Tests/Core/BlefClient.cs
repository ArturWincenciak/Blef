using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.Model;
using DealNumber = Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects.DealNumber;
using GameId = Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects.GameId;
using PlayerId = Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects.PlayerId;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;
    private PlayerId? _conwayPlayerId;


    private GameId? _gameId;
    private PlayerId? _grahamPlayerId;
    private PlayerId? _knuthPlayerId;
    private PlayerId? _riemannPlayerId;

    internal BlefClient(HttpClient httpClient) =>
        _httpClient = httpClient;

    async internal Task<NewGame.Result> NewGame()
    {
        var game = await _httpClient.NewGame();
        _gameId = new GameId(game.GameId);
        return game;
    }

    async internal Task<GetGame.Result> GetGameFlow() =>
        await _httpClient.GetGameFlow(_gameId!);

    async internal Task<GetDeal.Result> GetDealFlow(DealNumber dealNumber) =>
        await _httpClient.GetDealFlow(gameId: _gameId!, dealNumber);

    async internal Task<JoinGame.Result> JoinPlayer(WhichPlayer whichPlayer)
    {
        var player = await _httpClient.JoinPlayer(gameId: _gameId!, nick: new PlayerNick(whichPlayer.ToString()));
        SetPlayerId(whichPlayer, playerId: new PlayerId(player.PlayerId));
        return player;
    }

    async internal Task<StartFirstDeal.Result> StartFirstDeal() =>
        await _httpClient.StartFirstDeal(_gameId!);

    async internal Task<GetPlayerCards.Result> GetCards(WhichPlayer whichPlayer, DealNumber deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.GetCards(gameId: _gameId!, deal, playerId);
    }

    async internal Task Bid(WhichPlayer whichPlayer, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidWithSuccess(gameId: _gameId!, playerId, bid);
    }

    async internal Task Check(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.CheckWithSuccess(gameId: _gameId!, playerId);
    }

    private PlayerId GetPlayerId(WhichPlayer whichPlayer) =>
        whichPlayer switch
        {
            WhichPlayer.Knuth => _knuthPlayerId!,
            WhichPlayer.Graham => _grahamPlayerId!,
            WhichPlayer.Riemann => _riemannPlayerId!,
            WhichPlayer.Conway => _conwayPlayerId!,
            _ => throw new ArgumentOutOfRangeException(paramName: nameof(whichPlayer), whichPlayer,
                message: "Unknown player, please provide a valid player")
        };

    private void SetPlayerId(WhichPlayer whichPlayer, PlayerId playerId)
    {
        switch (whichPlayer)
        {
            case WhichPlayer.Knuth:
                _knuthPlayerId = playerId;
                break;
            case WhichPlayer.Graham:
                _grahamPlayerId = playerId;
                break;
            case WhichPlayer.Riemann:
                _riemannPlayerId = playerId;
                break;
            case WhichPlayer.Conway:
                _conwayPlayerId = playerId;
                break;
            default:
                throw new ArgumentOutOfRangeException(paramName: nameof(whichPlayer), whichPlayer,
                    message: "Unknown player, please provide a valid player");
        }
    }
}