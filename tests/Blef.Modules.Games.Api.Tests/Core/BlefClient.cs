using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;

    private Guid _gameId;

    private Guid _knuthPlayerId;

    private Guid _grahamPlayerId;

    private Guid _riemannPlayerId;

    private Guid _conwayPlayerId;

    internal BlefClient(HttpClient httpClient) =>
        _httpClient = httpClient;

    internal State GetState() =>
        new (_gameId, _knuthPlayerId, _grahamPlayerId, _riemannPlayerId, _conwayPlayerId);

    async internal Task NewGame() =>
        _gameId = await _httpClient.MakeNewGame();

    async internal Task<GetGameFlow.Result> GetGameFlow() =>
        await _httpClient.GetGameFlow(_gameId);

    async internal Task JoinPlayer(WhichPlayer whichPlayer)
    {
        var player = await _httpClient.JoinPlayer(_gameId, nick: whichPlayer.ToString());
        SetPlayerId(whichPlayer, player);
    }

    async internal Task<GetPlayerCards.Result> GetCards(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.GetCards(_gameId, playerId);
    }

    async internal Task Bid(WhichPlayer whichPlayer, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.Bid(_gameId, playerId, bid);
    }

    async internal Task Check(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.Check(_gameId, playerId);
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
        Guid GameId,
        Guid KnuthPlayerId,
        Guid GrahamPlayerId,
        Guid RiemannPlayerId,
        Guid ConwayPlayerId);
}