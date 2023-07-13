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
    private PlayerId? _plackPlayerId;
    private PlayerId? _riemannPlayerId;

    internal BlefClient(HttpClient httpClient, TestRecorder testRecorder)
    {
        _httpClient = httpClient;
        _testRecorder = testRecorder;
    }

    async internal Task NewGame()
    {
        var game = await _httpClient.NewGame(_testRecorder);
        _gameId = new GameId(game.GameId);
    }

    async internal Task GetGameFlow() =>
        await _httpClient.GetGameFlow(
            gameId: _gameId ?? throw new InvalidOperationException("Game has to be created"),
            _testRecorder);

    async internal Task GetGameFlow(GameId gameId) =>
        await _httpClient.GetGameFlow(gameId, _testRecorder);

    async internal Task GetDealFlow(DealNumber dealNumber) =>
        await _httpClient.GetDealFlow(
            gameId: _gameId ?? throw new InvalidOperationException("Game has to be created"),
            dealNumber, _testRecorder);

    async internal Task GetDealFlow(GameId gameId, DealNumber dealNumber) =>
        await _httpClient.GetDealFlow(gameId, dealNumber, _testRecorder);

    async internal Task JoinPlayer(WhichPlayer whichPlayer)
    {
        var result = await _httpClient.JoinPlayer(
            gameId: _gameId ?? throw new InvalidOperationException("Game has to be created"),
            nick: new PlayerNick(whichPlayer.ToString()), _testRecorder);

        if (result is JoinGame.Result player)
            SetPlayerId(whichPlayer, playerId: new PlayerId(player.PlayerId));
    }

    async internal Task StartFirstDeal() =>
        await _httpClient.StartFirstDeal(
            gameId: _gameId ?? throw new InvalidOperationException("Game has to be created"),
            _testRecorder);

    async internal Task GetCards(WhichPlayer whichPlayer, DealNumber deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.GetCards(
            gameId: _gameId ?? throw new InvalidOperationException("Game has to be created"),
            deal, playerId, _testRecorder);
    }

    async internal Task BidHighCard(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidHighCard(gameId: _gameId!, playerId, faceCard, _testRecorder);
    }

    public async Task BidHighCard(PlayerId player, FaceCard faceCard) =>
        await _httpClient.BidHighCard(gameId: _gameId!, player, faceCard, _testRecorder);

    public async Task BidPair(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidPair(gameId: _gameId!, playerId, faceCard, _testRecorder);
    }

    public async Task BidTwoPairs(WhichPlayer whichPlayer, FaceCard first, FaceCard second)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidTwoPairs(gameId: _gameId!, playerId, first, second, _testRecorder);
    }

    public async Task BidLowStraight(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidLowStraight(gameId: _gameId!, playerId, _testRecorder);
    }

    public async Task BidHighStraight(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidHighStraight(gameId: _gameId!, playerId, _testRecorder);
    }

    public async Task BidThreeOfAKind(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidThreeOfAKind(gameId: _gameId!, playerId, faceCard, _testRecorder);
    }

    public async Task BidFullHouse(WhichPlayer whichPlayer, FaceCard threeOfAKind, FaceCard pair)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidFullHouse(gameId: _gameId!, playerId, threeOfAKind, pair, _testRecorder);
    }

    public async Task BidFlush(WhichPlayer whichPlayer, Suit suit)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidFlush(gameId: _gameId!, playerId, suit, _testRecorder);
    }

    public async Task BidFourOfAKind(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidFourOfAKind(gameId: _gameId!, playerId, faceCard, _testRecorder);
    }

    public async Task BidStraightFlush(WhichPlayer whichPlayer, Suit suit)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidStraightFlush(gameId: _gameId!, playerId, suit, _testRecorder);
    }

    public async Task BidRoyalFlush(WhichPlayer whichPlayer, Suit suit)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.BidRoyalFlush(gameId: _gameId!, playerId, suit, _testRecorder);
    }

    async internal Task Check(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        await _httpClient.Check(gameId: _gameId!, playerId, _testRecorder);
    }

    private PlayerId GetPlayerId(WhichPlayer whichPlayer) =>
        whichPlayer switch
        {
            WhichPlayer.Knuth => _knuthPlayerId!,
            WhichPlayer.Graham => _grahamPlayerId!,
            WhichPlayer.Riemann => _riemannPlayerId!,
            WhichPlayer.Conway => _conwayPlayerId!,
            WhichPlayer.Planck => _plackPlayerId!,
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
                _plackPlayerId = playerId;
                break;
            default:
                throw new ArgumentOutOfRangeException(paramName: nameof(whichPlayer), whichPlayer,
                    message: "Unknown player, please provide a valid player");
        }
    }
}