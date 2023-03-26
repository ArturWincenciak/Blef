using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Tests.Core;

internal sealed class TestBuilder
{
    private readonly List<Func<Task>> _actions = new();
    private BlefClient _gameClient = null!;

    async internal Task Build()
    {
        var httpClient = new BlefApplicationFactory()
            .CreateClient();

        _gameClient = new BlefClient(httpClient);

        foreach (var action in _actions)
            await action();
    }

    internal TestBuilder NewGame()
    {
        _actions.Add(() => _gameClient.NewGame());
        return this;
    }

    public TestBuilder GetGameFlow()
    {
        _actions.Add(async () =>
        {
            var gameFlow = await _gameClient.GetGameFlow();
        });
        return this;
    }

    internal TestBuilder GetDealFlow(DealNumber deal, Action<GetDealFlow.Result, BlefClient.State>? with = null)
    {
        _actions.Add(async () =>
        {
            var dealFlow = await _gameClient.GetDealFlow(deal);
            var gameClientState = _gameClient.GetState();
            with?.Invoke(dealFlow, gameClientState);
        });
        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.JoinPlayer(whichPlayer));
        return this;
    }

    internal TestBuilder NewDeal(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.Deal(whichPlayer));
        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer, Deal deal, Action<GetPlayerCards.Result>? with = null)
    {
        _actions.Add(async () =>
        {
            var cards = await _gameClient.GetCards(whichPlayer, deal);
            with?.Invoke(cards);
        });
        return this;
    }

    internal TestBuilder Bid(WhichPlayer whichPlayer, Deal deal, string bid, Action<ProblemDetails>? with = null)
    {
        if (with is not null)
            _actions.Add(async () => with(await _gameClient.BidWithRuleViolation(whichPlayer, deal, bid)));
        else
            _actions.Add(() => _gameClient.BidWithSuccess(whichPlayer, deal, bid));

        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer, Deal deal, Action<ProblemDetails>? with = null)
    {
        if (with is not null)
            _actions.Add(async () => with(await _gameClient.CheckWithRuleViolation(whichPlayer, deal)));
        else
            _actions.Add(() => _gameClient.CheckWithSuccess(whichPlayer, deal));

        return this;
    }
}