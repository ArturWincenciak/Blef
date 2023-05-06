using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Events;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.AssertExtension;

namespace Blef.Modules.Games.Domain.Tests;

public class GameOverTests
{
    [Fact]
    public void GameOverWithOneSurvivingPlayerTest()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        PlayNotExistingLowStraightBid(game, firstPlayerJoined.Player.Id);
        game.Check(new(secondPlayerJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, firstPlayerJoined.Player.Id);
        game.Check(new(secondPlayerJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, firstPlayerJoined.Player.Id);
        game.Check(new(secondPlayerJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, firstPlayerJoined.Player.Id);
        game.Check(new(secondPlayerJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, firstPlayerJoined.Player.Id);

        // act
        var actual = game.Check(new(secondPlayerJoined.Player.Id));

        // assert
        var expectedWinner = secondPlayerJoined.Player.Id;
        AssertGameOver(game.Id, expectedWinner, actual);
        var expectedCheckingPlayer = secondPlayerJoined.Player.Id;
        var expectedLooser = firstPlayerJoined.Player.Id;
        AssertCheckPlaced(game.Id, new DealNumber(5), expectedCheckingPlayer, expectedLooser, actual);
    }

    private static void AssertGameOver(GameId expectedGameId, PlayerId expectedWinner, IEnumerable<IDomainEvent> actual)
    {
        var gameOver = actual.Single(@event => @event is GameOver) as GameOver;
        Assert.Equal(expectedGameId, gameOver!.Game);
        Assert.Equal(expectedWinner, gameOver.Winner.Id);
    }
}