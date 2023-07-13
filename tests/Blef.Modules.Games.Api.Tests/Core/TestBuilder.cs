using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestBuilder
{
    private static string _noArgument = nameof(_noArgument);
    private readonly List<Func<Task>> _actions = new();
    private readonly TestRecorder _testRecorder = new();
    private BlefClient _gameClient = null!;

    async internal Task<IReadOnlyCollection<TestRecorder.TestResult>> Build()
    {
        var httpClient = new BlefApplicationFactory()
            .CreateClient();

        _gameClient = new BlefClient(httpClient, _testRecorder);

        foreach (var action in _actions)
            await action();

        return _testRecorder.Actual;
    }

    internal TestBuilder NewGame()
    {
        _actions.Add(async () => await _gameClient.NewGame());
        return this;
    }

    public TestBuilder GetGameFlow()
    {
        _actions.Add(async () => await _gameClient.GetGameFlow());
        return this;
    }

    public TestBuilder GetGameFlow(GameId gameId)
    {
        _actions.Add(async () => await _gameClient.GetGameFlow(gameId));
        return this;
    }

    internal TestBuilder GetDealFlow(DealNumber deal)
    {
        _actions.Add(async () => await _gameClient.GetDealFlow(deal));
        return this;
    }

    internal TestBuilder GetDealFlow(GameId gameId, DealNumber deal)
    {
        _actions.Add(async () => await _gameClient.GetDealFlow(gameId, deal));
        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(async () => await _gameClient.JoinPlayer(whichPlayer));
        return this;
    }

    internal TestBuilder NewDeal()
    {
        _actions.Add(async () => await _gameClient.StartFirstDeal());
        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer, DealNumber deal)
    {
        _actions.Add(async () => await _gameClient.GetCards(whichPlayer, deal));
        return this;
    }

    public TestBuilder BidHighCard(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () => await _gameClient.BidHighCard(whichPlayer, faceCard));
        return this;
    }

    public TestBuilder BidHighCard(GameId gameId, WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () => await _gameClient.BidHighCard(gameId, whichPlayer, faceCard));
        return this;
    }

    public TestBuilder BidHighCard(PlayerId player, FaceCard faceCard)
    {
        _actions.Add(async () => await _gameClient.BidHighCard(player, faceCard));
        return this;
    }

    public TestBuilder BidPair(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () => await _gameClient.BidPair(whichPlayer, faceCard));
        return this;
    }

    public TestBuilder BidTwoPairs(WhichPlayer whichPlayer, FaceCard first, FaceCard second)
    {
        _actions.Add(async () => await _gameClient.BidTwoPairs(whichPlayer, first, second));
        return this;
    }

    public TestBuilder BidLowStraight(WhichPlayer whichPlayer)
    {
        _actions.Add(async () => await _gameClient.BidLowStraight(whichPlayer));
        return this;
    }

    public TestBuilder BidHighStraight(WhichPlayer whichPlayer)
    {
        _actions.Add(async () => await _gameClient.BidHighStraight(whichPlayer));
        return this;
    }

    public TestBuilder BidThreeOfAKind(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () => await _gameClient.BidThreeOfAKind(whichPlayer, faceCard));
        return this;
    }

    public TestBuilder BidFullHouse(WhichPlayer whichPlayer, FaceCard threeOfAKind, FaceCard pair)
    {
        _actions.Add(async () => await _gameClient.BidFullHouse(whichPlayer, threeOfAKind, pair));
        return this;
    }

    public TestBuilder BidFlush(WhichPlayer whichPlayer, Suit suit)
    {
        _actions.Add(async () => await _gameClient.BidFlush(whichPlayer, suit));
        return this;
    }

    public TestBuilder BidFourOfAKind(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () => await _gameClient.BidFourOfAKind(whichPlayer, faceCard));
        return this;
    }

    public TestBuilder BidStraightFlush(WhichPlayer whichPlayer, Suit suit)
    {
        _actions.Add(async () => await _gameClient.BidStraightFlush(whichPlayer, suit));
        return this;
    }

    public TestBuilder BidRoyalFlush(WhichPlayer whichPlayer, Suit suit)
    {
        _actions.Add(async () => await _gameClient.BidRoyalFlush(whichPlayer, suit));
        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer)
    {
        _actions.Add(async () => await _gameClient.Check(whichPlayer));
        return this;
    }
}