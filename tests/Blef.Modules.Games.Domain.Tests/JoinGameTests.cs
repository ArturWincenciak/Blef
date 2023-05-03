using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.Tests.Mocks;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class JoinGameTests
{
    private static Game GivenGame()
    {
        var gameGuid = Guid.Parse("8AFD62F2-A00B-4551-B049-6F4DB0D47CE3");
        var gameId = new GameId(gameGuid);
        var croupier = new Croupier(new DeckFactoryMock());
        return new Game(gameId, croupier);
    }

    [Fact]
    public void OnePlayerJoinGameTest()
    {
        // arrange
        var game = GivenGame();
        var playerNick = "Graham";

        // act
        var gamePlayerJoined = game.Join(new PlayerNick(playerNick));

        // assert
        AssertJoinedPlayer(game.GameId, playerNick, gamePlayerJoined);
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
        AssertJoinedPlayer(game.GameId, playerNick1, gamePlayerJoined1);
        AssertJoinedPlayer(game.GameId, playerNick2, gamePlayerJoined2);
        AssertJoinedPlayer(game.GameId, playerNick3, gamePlayerJoined3);
        AssertJoinedPlayer(game.GameId, playerNick4, gamePlayerJoined4);
    }

    [Fact]
    public void CannotJoinGameMoreThenMaxPlayersTest()
    {
        // arrange
        var game = GivenGame();

        // act
        Assert.Throws<MaxGamePlayersReachedException>(() =>
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
        Assert.Throws<PlayerAlreadyJoinedTheGameException>(() =>
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
        game.Join(new("Graham"));
        game.Join(new("Knuth"));
        game.StartFirstDeal();

        // assert
        Assert.Throws<JoinGameThatIsAlreadyStartedException>(() =>
        {
            // act
            game.Join(new("Conway"));
        });
    }

    private static void AssertJoinedPlayer(GameId expectedGameId, string expectedNick, GamePlayerJoined actualEvent)
    {
        Assert.Equal(expectedGameId.Id, actualEvent.GameId);
        Assert.Equal(expectedNick, actualEvent.Nick);
        Assert.NotEqual(Guid.Empty, actualEvent.PlayerId);
    }
}