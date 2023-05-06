﻿using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.Tests.Mocks;

namespace Blef.Modules.Games.Domain.Tests.Extensions;

internal static class GameFactory
{
    public static Game GivenGame()
    {
        var gameGuid = Guid.Parse("8AFD62F2-A00B-4551-B049-6F4DB0D47CE3");
        var gameId = new GameId(gameGuid);
        var croupier = new Croupier(new DeckFactoryMock());
        return new Game(gameId, croupier);
    }

    public static (
        Game Game,
        GamePlayerJoined FirstPlayerJoined,
        GamePlayerJoined SecondPlayerJoined)
        GivenGameWithTwoPlayersWithFirstDeal()
    {
        var game = GivenGame();
        var grahamJoined = game.Join(new("Graham"));
        var knuthJoined = game.Join(new("Knuth"));
        game.StartFirstDeal();
        return (game, grahamJoined, knuthJoined);
    }
}