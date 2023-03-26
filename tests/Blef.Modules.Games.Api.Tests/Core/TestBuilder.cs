using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestBuilder
{
    private readonly TestRecorder _testResult = new();
    private readonly List<Func<Task>> _actions = new();
    private BlefClient _gameClient = null!;

    private static string NoArgument = nameof(NoArgument);
    private static TestRecorder.EmptyCommandResult Success = new ("OK");

    async internal Task<IEnumerable<TestRecorder.TestResult>> Build()
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
            _testResult.Record(nameof(NewGame), NoArgument, game);
        });

        return this;
    }

    public TestBuilder GetGameFlow()
    {
        _actions.Add(async () =>
        {
            var gameFlow = await _gameClient.GetGameFlow();
            _testResult.Record(nameof(GetGameFlow), NoArgument, gameFlow);
        });

        return this;
    }

    internal TestBuilder GetDealFlow(DealNumber deal)
    {
        _actions.Add(async () =>
        {
            var dealFlow = await _gameClient.GetDealFlow(deal);
            _testResult.Record(nameof(GetDealFlow), deal, dealFlow);
        });

        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(async () =>
        {
            var player = await _gameClient.JoinPlayer(whichPlayer);
            _testResult.Record(nameof(JoinPlayer), whichPlayer, player);
        });

        return this;
    }

    internal TestBuilder NewDeal(WhichPlayer whichPlayer)
    {
        _actions.Add(async () =>
        {
            var deal = await _gameClient.Deal(whichPlayer);
            _testResult.Record(nameof(NewDeal), whichPlayer, deal);
        });

        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer, Deal deal)
    {
        _actions.Add(async () =>
        {
            var cards = await _gameClient.GetCards(whichPlayer, deal);
            _testResult.Record(nameof(GetCards), new { whichPlayer, deal }, cards);
        });

        return this;
    }

    internal TestBuilder Bid(WhichPlayer whichPlayer, Deal deal, string bid)
    {
        _actions.Add(async () =>
        {
             await _gameClient.Bid(whichPlayer, deal, bid);
             _testResult.Record(nameof(Bid), new { whichPlayer, deal, bid }, Success);
        });

        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer, Deal deal)
    {
        _actions.Add(async () =>
        {
            await _gameClient.Check(whichPlayer, deal);
            _testResult.Record(nameof(Check), new { whichPlayer, deal }, Success);
        });

        return this;
    }
}