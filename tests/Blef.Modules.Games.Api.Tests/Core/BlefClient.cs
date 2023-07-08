using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;
using Blef.Modules.Games.Application.Commands;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class BlefClient
{
    private readonly HttpClient _httpClient;
    private PlayerId? _conwayPlayerId;

    private GameId? _gameId;
    private PlayerId? _grahamPlayerId;
    private PlayerId? _knuthPlayerId;
    private PlayerId? _riemannPlayerId;
    private PlayerId? _plackPlayerId;

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

    async internal Task<object> JoinPlayer(WhichPlayer whichPlayer)
    {
        var result = await _httpClient.JoinPlayer(gameId: _gameId!, nick: new PlayerNick(whichPlayer.ToString()));

        if (result is JoinGame.Result player)
            SetPlayerId(whichPlayer, playerId: new PlayerId(player.PlayerId));

        return result;
    }

    async internal Task<object> StartFirstDeal() =>
        await _httpClient.StartFirstDeal(_gameId!);

    async internal Task<GetPlayerCards.Result> GetCards(WhichPlayer whichPlayer, DealNumber deal)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.GetCards(gameId: _gameId!, deal, playerId);
    }

    async internal Task<object> BidHighCard(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidHighCard(gameId: _gameId!, playerId, faceCard);
    }

    public async Task<object> BidHighCard(PlayerId player, FaceCard faceCard) =>
        await _httpClient.BidHighCard(gameId: _gameId!, player, faceCard);

    public async Task<object> BidPair(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidPair(gameId: _gameId!, playerId, faceCard);
    }

    public async Task<object> BidTwoPairs(WhichPlayer whichPlayer, FaceCard first, FaceCard second)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidTwoPairs(gameId: _gameId!, playerId, first, second);
    }

    public async Task<object> BidLowStraight(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidLowStraight(gameId: _gameId!, playerId);
    }

    public async Task<object> BidHighStraight(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidHighStraight(gameId: _gameId!, playerId);
    }

    public async Task<object> BidThreeOfAKind(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidThreeOfAKind(gameId: _gameId!, playerId, faceCard);
    }

    public async Task<object> BidFullHouse(WhichPlayer whichPlayer, FaceCard threeOfAKind, FaceCard pair)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidFullHouse(gameId: _gameId!, playerId, threeOfAKind, pair);
    }

    public async Task<object> BidFlush(WhichPlayer whichPlayer, Suit suit)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidFlush(gameId: _gameId!, playerId, suit);
    }

    public async Task<object> BidFourOfAKind(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidFourOfAKind(gameId: _gameId!, playerId, faceCard);
    }

    public async Task<object> BidStraightFlush(WhichPlayer whichPlayer, Suit suit)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidStraightFlush(gameId: _gameId!, playerId, suit);
    }

    public async Task<object> BidRoyalFlush(WhichPlayer whichPlayer, Suit suit)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.BidRoyalFlush(gameId: _gameId!, playerId, suit);
    }

    async internal Task<object> Check(WhichPlayer whichPlayer)
    {
        var playerId = GetPlayerId(whichPlayer);
        return await _httpClient.CheckWithSuccess(gameId: _gameId!, playerId);
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