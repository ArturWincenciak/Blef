﻿using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class JoinGameTests
{
    [Fact]
    public void OnePlayerJoinGameTest()
    {
        // arrange
        var game = GivenGame();
        const string playerNick = "Graham";

        // act
        var gamePlayerJoined = game.Join(new(playerNick));

        // assert
        AssertJoinedPlayer(game.Id, expectedNick: new(playerNick), gamePlayerJoined);
    }

    [Fact]
    public void MaxPlayersJoinGameTest()
    {
        // arrange
        var game = GivenGame();
        const string playerNick1 = "First player";
        const string playerNick2 = "Second player";
        const string playerNick3 = "Third player";
        const string playerNick4 = "Fourth player";

        // act
        var gamePlayerJoined1 = game.Join(new(playerNick1));
        var gamePlayerJoined2 = game.Join(new(playerNick2));
        var gamePlayerJoined3 = game.Join(new(playerNick3));
        var gamePlayerJoined4 = game.Join(new(playerNick4));

        // assert
        AssertJoinedPlayer(game.Id, expectedNick: new(playerNick1), gamePlayerJoined1);
        AssertJoinedPlayer(game.Id, expectedNick: new(playerNick2), gamePlayerJoined2);
        AssertJoinedPlayer(game.Id, expectedNick: new(playerNick3), gamePlayerJoined3);
        AssertJoinedPlayer(game.Id, expectedNick: new(playerNick4), gamePlayerJoined4);
    }

    [Fact]
    public void CannotJoinGameMoreThenMaxPlayersTest()
    {
        // arrange
        var game = GivenGame();
        game.Join(new("Nick 1"));
        game.Join(new("Nick 2"));
        game.Join(new("Nick 3"));
        game.Join(new("Nick 4"));

        // act, assert
        Assert.Throws<TooManyPlayersException>(() =>
            game.Join(new("Nick 5")));
    }

    [Fact]
    public void CannotJoinGameTheSamePlayerTwiceTest()
    {
        // arrange
        var game = GivenGame();
        game.Join(new("Graham"));

        // act, assert
        Assert.Throws<PlayerAlreadyJoinedException>(() =>
            game.Join(new("Graham")));
    }

    [Fact]
    public void CannotJoinToAlreadyStartedGameTest()
    {
        // arrange
        var game = GivenGame();
        game.Join(new("Graham"));
        game.Join(new("Knuth"));
        game.StartFirstDeal();

        // act, assert
        Assert.Throws<JoinGameAlreadyStartedException>(() =>
            game.Join(new("Conway")));
    }

    private static void AssertJoinedPlayer(GameId expectedGameId, PlayerNick expectedNick, GamePlayerJoined actualEvent)
    {
        Assert.Equal(expectedGameId, actualEvent.Game);
        Assert.Equal(expectedNick, actualEvent.Player.Nick);
    }
}