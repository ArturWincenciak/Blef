namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;

    private Guid _gameId;
    private Guid _knuthPlayerId;
    private Guid _grahamPlayerId;
    private Guid _riemannPlayerId;
    private Guid _conwayPlayerId;

    public BlefClient(HttpClient httpClient) =>
        _httpClient = httpClient;

    public async Task NewGame() =>
        _gameId = await _httpClient.MakeNewGame();

    public async Task GetGameFlow() =>
        await _httpClient.GetGameFlow(_gameId);

    public async Task JoinPlayer(WhichPlayer whichPlayer)
    {
        var player = await _httpClient.JoinPlayer(_gameId, nick: whichPlayer.ToString());
        SetPlayerId(whichPlayer, player);
    }

    public async Task GetCards(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.GetCards(_gameId, playerId);
    }

    public async Task Bid(WhichPlayer whichPlayer, string bid)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.Bid(_gameId, playerId, bid);
    }

    public async Task Check(WhichPlayer whichPlayer)
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
}