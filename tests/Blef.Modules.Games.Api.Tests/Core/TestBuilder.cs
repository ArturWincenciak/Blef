using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestBuilder
{
    private readonly List<Func<Task>> _actions = new();
    private readonly TestRecorder _testRecorder = new();
    private BlefClient _gameClient = null!;

    async internal Task<TestRecorder.TestResult> Build()
    {
        var httpClient = new BlefApplicationFactory()
            .CreateClient();

        _gameClient = new BlefClient(httpClient, _testRecorder);

        foreach (var action in _actions)
            await action();

        return _testRecorder.Actual;
    }

    internal TestBuilder NewGame(string? description = null)
    {
        _actions.Add(async () => await _gameClient.NewGame(description));
        return this;
    }

    public TestBuilder GetGameFlow(string? description = null)
    {
        _actions.Add(async () => await _gameClient.GetGameFlow(description));
        return this;
    }

    public TestBuilder GetGameFlow(GameId gameId, string? description = null)
    {
        _actions.Add(async () => await _gameClient.GetGameFlow(gameId, description));
        return this;
    }

    internal TestBuilder GetDealFlow(DealNumber deal, string? description = null)
    {
        _actions.Add(async () => await _gameClient.GetDealFlow(deal, description));
        return this;
    }

    internal TestBuilder GetDealFlow(GameId gameId, DealNumber deal, string? description = null)
    {
        _actions.Add(async () => await _gameClient.GetDealFlow(gameId, deal, description));
        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer, string? description = null)
    {
        _actions.Add(async () => await _gameClient.JoinPlayer(whichPlayer, description));
        return this;
    }

    internal TestBuilder JoinPlayer(GameId gameId, WhichPlayer whichPlayer)
    {
        _actions.Add(async () => await _gameClient.JoinPlayer(gameId, whichPlayer));
        return this;
    }

    internal TestBuilder NewDeal(string? description = null)
    {
        _actions.Add(async () => await _gameClient.StartFirstDeal(description));
        return this;
    }

    internal TestBuilder NewDeal(GameId gameId)
    {
        _actions.Add(async () => await _gameClient.StartFirstDeal(gameId));
        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer, DealNumber deal, string? description = null)
    {
        _actions.Add(async () => await _gameClient.GetCards(whichPlayer, deal, description));
        return this;
    }

    internal TestBuilder GetCards(PlayerId playerId, DealNumber deal, string? description = null)
    {
        _actions.Add(async () => await _gameClient.GetCards(playerId, deal, description));
        return this;
    }

    internal TestBuilder GetCards(GameId gameId, WhichPlayer whichPlayer, DealNumber deal)
    {
        _actions.Add(async () => await _gameClient.GetCards(gameId, whichPlayer, deal));
        return this;
    }

    public TestBuilder BidHighCard(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidHighCard(whichPlayer, faceCard, description));
        return this;
    }

    public TestBuilder BidHighCard(GameId gameId, WhichPlayer whichPlayer, FaceCard faceCard,
        string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidHighCard(gameId, whichPlayer, faceCard, description));
        return this;
    }

    public TestBuilder BidHighCard(PlayerId player, FaceCard faceCard, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidHighCard(player, faceCard, description));
        return this;
    }

    public TestBuilder BidPair(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidPair(whichPlayer, faceCard, description));
        return this;
    }

    public TestBuilder BidTwoPairs(WhichPlayer whichPlayer, FaceCard first, FaceCard second, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidTwoPairs(whichPlayer, first, second, description));
        return this;
    }

    public TestBuilder BidLowStraight(WhichPlayer whichPlayer, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidLowStraight(whichPlayer, description));
        return this;
    }

    public TestBuilder BidHighStraight(WhichPlayer whichPlayer, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidHighStraight(whichPlayer, description));
        return this;
    }

    public TestBuilder BidThreeOfAKind(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidThreeOfAKind(whichPlayer, faceCard, description));
        return this;
    }

    public TestBuilder BidFullHouse(WhichPlayer whichPlayer, FaceCard threeOfAKind, FaceCard pair,
        string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidFullHouse(whichPlayer, threeOfAKind, pair, description));
        return this;
    }

    public TestBuilder BidFlush(WhichPlayer whichPlayer, Suit suit, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidFlush(whichPlayer, suit, description));
        return this;
    }

    public TestBuilder BidFourOfAKind(WhichPlayer whichPlayer, FaceCard faceCard, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidFourOfAKind(whichPlayer, faceCard, description));
        return this;
    }

    public TestBuilder BidStraightFlush(WhichPlayer whichPlayer, Suit suit, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidStraightFlush(whichPlayer, suit, description));
        return this;
    }

    public TestBuilder BidRoyalFlush(WhichPlayer whichPlayer, Suit suit, string? description = null)
    {
        _actions.Add(async () => await _gameClient.BidRoyalFlush(whichPlayer, suit, description));
        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer, string? description = null)
    {
        _actions.Add(async () => await _gameClient.Check(whichPlayer, description));
        return this;
    }

    internal TestBuilder Check(PlayerId player, string? description = null)
    {
        _actions.Add(async () => await _gameClient.Check(player, description));
        return this;
    }

    internal TestBuilder Check(GameId gameId, WhichPlayer whichPlayer, string? description = null)
    {
        _actions.Add(async () => await _gameClient.Check(gameId, whichPlayer, description));
        return this;
    }

    internal TestBuilder Home()
    {
        _actions.Add(async () => await _gameClient.Home());
        return this;
    }
}