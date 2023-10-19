using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;
using Blef.Modules.Games.Application.Commands;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;
    private readonly TestRecorder _testRecorder;
    private PlayerId? _conwayPlayerId;
    private GameId? _gameId;
    private PlayerId? _grahamPlayerId;
    private PlayerId? _knuthPlayerId;
    private PlayerId? _planckPlayerId;
    private PlayerId? _riemannPlayerId;

    private GameId GameId => _gameId ?? throw new InvalidOperationException("Game has to be created");

    internal BlefClient(HttpClient httpClient, TestRecorder testRecorder)
    {
        _httpClient = httpClient;
        _testRecorder = testRecorder;
    }

    async internal Task NewGame(string? description = null)
    {
        var game = await _httpClient.NewGame(_testRecorder, description);
        _gameId = new GameId(game.GameId);
    }

    async internal Task GetGameFlow(string? description = null) =>
        await _httpClient.GetGameFlow(GameId, _testRecorder, description);

    async internal Task GetGameFlow(GameId gameId, string? description = null) =>
        await _httpClient.GetGameFlow(gameId, _testRecorder, description);

    async internal Task GetDealFlow(DealNumber dealNumber, string? description = null) =>
        await _httpClient.GetDealFlow(GameId, dealNumber, _testRecorder, description);

    async internal Task GetDealFlow(GameId gameId, DealNumber dealNumber, string? description = null) =>
        await _httpClient.GetDealFlow(gameId, dealNumber, _testRecorder, description);

    async internal Task JoinPlayer(WhichPlayer whichPlayer, string? description = null) =>
        await JoinPlayer(GameId, whichPlayer, description);

    async internal Task JoinPlayer(GameId gameId, WhichPlayer whichPlayer, string? description = null)
    {
        var result = await _httpClient.JoinPlayer(
            gameId, nick: new PlayerNick(whichPlayer.ToString()), _testRecorder, description);

        if (result is JoinGame.Result player)
            SetPlayerId(whichPlayer, playerId: new PlayerId(player.PlayerId));
    }

    async internal Task StartFirstDeal(string? description = null) =>
        await StartFirstDeal(GameId, description);

    async internal Task StartFirstDeal(GameId gameId, string? description = null) =>
        await _httpClient.StartFirstDeal(gameId, _testRecorder, description);

    async internal Task GetCards(WhichPlayer whichPlayer, DealNumber deal, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.GetCards(GameId, deal, playerId, _testRecorder, description);
    }

    async internal Task GetCards(PlayerId playerId, DealNumber deal, string? description) =>
        await _httpClient.GetCards(GameId, deal, playerId, _testRecorder, description);

    async internal Task GetCards(GameId gameId, WhichPlayer whichPlayer, DealNumber deal, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.GetCards(gameId, deal, playerId, _testRecorder, description);
    }

    async internal Task BidHighCard(WhichPlayer whichPlayer, FaceCard faceCard, string? description) =>
        await BidHighCard(GameId, whichPlayer, faceCard, description);

    async internal Task BidHighCard(GameId gameId, WhichPlayer whichPlayer, FaceCard faceCard, string? description)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidHighCard(gameId, playerId, faceCard, _testRecorder, description);
    }

    public async Task BidHighCard(PlayerId player, FaceCard faceCard, string? description = null) =>
        await _httpClient.BidHighCard(GameId, player, faceCard, _testRecorder, description);

    public async Task BidPair(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidPair(GameId, playerId, faceCard, _testRecorder, description);
    }

    public async Task BidTwoPairs(WhichPlayer whichPlayer, FaceCard first, FaceCard second, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidTwoPairs(GameId, playerId, first, second, _testRecorder, description);
    }

    public async Task BidLowStraight(WhichPlayer whichPlayer, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidLowStraight(GameId, playerId, _testRecorder, description);
    }

    public async Task BidHighStraight(WhichPlayer whichPlayer, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidHighStraight(GameId, playerId, _testRecorder, description);
    }

    public async Task BidThreeOfAKind(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidThreeOfAKind(GameId, playerId, faceCard, _testRecorder, description);
    }

    public async Task BidFullHouse(WhichPlayer whichPlayer, FaceCard threeOfAKind, FaceCard pair,
        string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidFullHouse(GameId, playerId, threeOfAKind, pair, _testRecorder, description);
    }

    public async Task BidFlush(WhichPlayer whichPlayer, Suit suit, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidFlush(GameId, playerId, suit, _testRecorder, description);
    }

    public async Task BidFourOfAKind(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidFourOfAKind(GameId, playerId, faceCard, _testRecorder, description);
    }

    public async Task BidStraightFlush(WhichPlayer whichPlayer, Suit suit, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidStraightFlush(GameId, playerId, suit, _testRecorder, description);
    }

    public async Task BidRoyalFlush(WhichPlayer whichPlayer, Suit suit, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidRoyalFlush(GameId, playerId, suit, _testRecorder, description);
    }

    async internal Task Check(WhichPlayer whichPlayer, string? description = null) =>
        await Check(GameId, whichPlayer, description);

    async internal Task Check(GameId gameId, WhichPlayer whichPlayer, string? description = null)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.Check(gameId, playerId, _testRecorder, description);
    }

    async internal Task Check(PlayerId playerId, string? description = null) =>
        await _httpClient.Check(GameId, playerId, _testRecorder, description);

    async internal Task Home() =>
        await _httpClient.Home(_testRecorder);

    private PlayerId GetPlayerId(WhichPlayer whichPlayer) =>
        whichPlayer switch
        {
            WhichPlayer.Knuth => _knuthPlayerId!,
            WhichPlayer.Graham => _grahamPlayerId!,
            WhichPlayer.Riemann => _riemannPlayerId!,
            WhichPlayer.Conway => _conwayPlayerId!,
            WhichPlayer.Planck => _planckPlayerId!,
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
            case WhichPlayer.Planck:
                _planckPlayerId = playerId;
                break;
            default:
                throw new ArgumentOutOfRangeException(paramName: nameof(whichPlayer), whichPlayer,
                    message: "Unknown player, please provide a valid player");
        }
    }
}