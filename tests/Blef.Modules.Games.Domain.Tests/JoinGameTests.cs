using Blef.Modules.Games.Domain.Events;
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
        var playerNick = "Graham";

        // act
        var gamePlayerJoined = game.Join(new PlayerNick(playerNick));

        // assert
        AssertJoinedPlayer(game.Id, expectedNick: new PlayerNick(playerNick), gamePlayerJoined);
    }

    [Fact]
    public void MaxPlayersJoinGameTest()
    {
        // arrange
        var game = GivenGame();
        var playerNick1 = "First player";
        var playerNick2 = "Second player";
        var playerNick3 = "Third player";
        var playerNick4 = "Fourth player";

        // act
        var gamePlayerJoined1 = game.Join(new PlayerNick(playerNick1));
        var gamePlayerJoined2 = game.Join(new PlayerNick(playerNick2));
        var gamePlayerJoined3 = game.Join(new PlayerNick(playerNick3));
        var gamePlayerJoined4 = game.Join(new PlayerNick(playerNick4));

        // assert
        AssertJoinedPlayer(game.Id, expectedNick: new PlayerNick(playerNick1), gamePlayerJoined1);
        AssertJoinedPlayer(game.Id, expectedNick: new PlayerNick(playerNick2), gamePlayerJoined2);
        AssertJoinedPlayer(game.Id, expectedNick: new PlayerNick(playerNick3), gamePlayerJoined3);
        AssertJoinedPlayer(game.Id, expectedNick: new PlayerNick(playerNick4), gamePlayerJoined4);
    }

    [Fact]
    public void CannotJoinGameMoreThenMaxPlayersTest()
    {
        // arrange
        var game = GivenGame();

        // act
        Assert.Throws<TooManyPlayersException>(() =>
        {
            game.Join(new PlayerNick("Nick 1"));
            game.Join(new PlayerNick("Nick 2"));
            game.Join(new PlayerNick("Nick 3"));
            game.Join(new PlayerNick("Nick 4"));
            game.Join(new PlayerNick("Nick 5"));
        });
    }

    [Fact]
    public void CannotJoinGameTheSamePlayerTwiceTest()
    {
        // arrange
        var game = GivenGame();

        // act
        Assert.Throws<PlayerAlreadyJoinedException>(() =>
        {
            game.Join(new PlayerNick("Graham"));
            game.Join(new PlayerNick("Graham"));
        });
    }

    [Fact]
    public void CannotJoinToAlreadyStartedGameTest()
    {
        // arrange
        var game = GivenGame();
        game.Join(new PlayerNick("Graham"));
        game.Join(new PlayerNick("Knuth"));
        game.StartFirstDeal();

        // assert
        Assert.Throws<JoinGameAlreadyStartedException>(() =>
        {
            // act
            game.Join(new PlayerNick("Conway"));
        });
    }

    private static void AssertJoinedPlayer(GameId expectedGameId, PlayerNick expectedNick, GamePlayerJoined actualEvent)
    {
        Assert.Equal(expectedGameId, actualEvent.Game);
        Assert.Equal(expectedNick, actualEvent.Player.Nick);
    }
}