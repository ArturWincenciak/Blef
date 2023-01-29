﻿namespace Blef.Modules.Games.Api.Tests;

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

    internal TestBuilder GetGameFlow()
    {
        _actions.Add(() => _gameClient.GetGameFlow());
        return this;
    }

    internal TestBuilder JoinPlayer(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.JoinPlayer(whichPlayer));
        return this;
    }

    internal TestBuilder GetCards(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.GetCards(whichPlayer));
        return this;
    }

    internal TestBuilder Bid(WhichPlayer whichPlayer, string bid)
    {
        _actions.Add(() => _gameClient.Bid(whichPlayer, bid));
        return this;
    }

    internal TestBuilder Check(WhichPlayer whichPlayer)
    {
        _actions.Add(() => _gameClient.Check(whichPlayer));
        return this;
    }
}