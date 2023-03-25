﻿using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
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

    internal TestBuilder GetGameFlow(Action<GetGameFlow.Result, BlefClient.State>? with = null)
    {
        _actions.Add(async () =>
        {
            var gameFlow = await _gameClient.GetGameFlow();
            var gameClientState = _gameClient.GetState();
            with?.Invoke(gameFlow, gameClientState);
        });
        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.JoinPlayer(whichPlayer));
        return this;
    }

    internal TestBuilder Deal(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.Deal(whichPlayer));
        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer, int deal, Action<GetPlayerCards.Result>? with = null)
    {
        _actions.Add(async () =>
        {
            var cards = await _gameClient.GetCards(whichPlayer, deal);
            with?.Invoke(cards);
        });
        return this;
    }

    internal TestBuilder Bid(WhichPlayer whichPlayer, int deal, string bid, Action<ProblemDetails>? with = null)
    {
        if (with is not null)
            _actions.Add(async () => with(await _gameClient.BidWithRuleViolation(whichPlayer, deal, bid)));
        else
            _actions.Add(() => _gameClient.BidWithSuccess(whichPlayer, deal, bid));

        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer, Action<ProblemDetails>? with = null)
    {
        if (with is not null)
            _actions.Add(async () => with(await _gameClient.CheckWithRuleViolation(whichPlayer)));
        else
            _actions.Add(() => _gameClient.CheckWithSuccess(whichPlayer));

        return this;
    }
}