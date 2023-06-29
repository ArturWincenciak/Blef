using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestBuilder
{
    private static string _noArgument = nameof(_noArgument);
    private readonly List<Func<Task>> _actions = new();
    private readonly TestRecorder _testResult = new();
    private BlefClient _gameClient = null!;

    async internal Task<IReadOnlyCollection<TestRecorder.TestResult>> Build()
    {
        var httpClient = new BlefApplicationFactory()
            .CreateClient();

        _gameClient = new BlefClient(httpClient);

        foreach (var action in _actions)
            await action();

        return _testResult.Actual;
    }

    internal TestBuilder NewGame()
    {
        _actions.Add(async () =>
        {
            var game = await _gameClient.NewGame();
            _testResult.Record(action: nameof(NewGame), _noArgument, game);
        });

        return this;
    }

    public TestBuilder GetGameFlow()
    {
        _actions.Add(async () =>
        {
            var gameFlow = await _gameClient.GetGameFlow();
            _testResult.Record(action: nameof(GetGameFlow), _noArgument, gameFlow);
        });

        return this;
    }

    internal TestBuilder GetDealFlow(DealNumber deal)
    {
        _actions.Add(async () =>
        {
            var dealFlow = await _gameClient.GetDealFlow(deal);
            _testResult.Record(action: nameof(GetDealFlow), deal, dealFlow);
        });

        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(async () =>
        {
            var player = await _gameClient.JoinPlayer(whichPlayer);
            _testResult.Record(action: nameof(JoinPlayer), whichPlayer, player);
        });

        return this;
    }

    internal TestBuilder NewDeal()
    {
        _actions.Add(async () =>
        {
            var deal = await _gameClient.StartFirstDeal();
            _testResult.Record(action: nameof(NewDeal), _noArgument, deal);
        });

        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer, DealNumber deal)
    {
        _actions.Add(async () =>
        {
            var cards = await _gameClient.GetCards(whichPlayer, deal);
            _testResult.Record(action: nameof(GetCards), argument: new {whichPlayer, deal}, cards);
        });

        return this;
    }

    internal TestBuilder Bid(WhichPlayer whichPlayer, string bid)
    {
        _actions.Add(async () =>
        {
            var result = await _gameClient.Bid(whichPlayer, bid);
            _testResult.Record(action: nameof(Bid), argument: new {whichPlayer, bid}, result);
        });

        return this;
    }

    public TestBuilder BidHighCard(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () =>
        {
            var result = await _gameClient.BidHighCard(whichPlayer, faceCard);
            _testResult.Record(action: nameof(BidHighCard), argument: new {whichPlayer, faceCard}, result);
        });

        return this;
    }

    public TestBuilder BidPair(WhichPlayer whichPlayer, FaceCard faceCard)
    {
        _actions.Add(async () =>
        {
            var result = await _gameClient.BidPair(whichPlayer, faceCard);
            _testResult.Record(action: nameof(BidPair), argument: new {whichPlayer, faceCard}, result);
        });

        return this;
    }

    public TestBuilder BidTwoPairs(WhichPlayer whichPlayer, FaceCard first, FaceCard second)
    {
        _actions.Add(async () =>
        {
            var result = await _gameClient.BidTwoPairs(whichPlayer, first, second);
            _testResult.Record(action: nameof(BidTwoPairs), argument: new {whichPlayer, first, second}, result);
        });

        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer)
    {
        _actions.Add(async () =>
        {
            var result = await _gameClient.Check(whichPlayer);
            _testResult.Record(action: nameof(Check), argument: new {whichPlayer}, result);
        });

        return this;
    }
}