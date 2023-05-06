using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class ValidateGameInProgressTests
{
    [Fact]
    public void CanMakeMoveWhenGameInProgressTest()
    {
        TestCase((game, player1, _) => PlayExistingHighCardBid(game, player1));
        TestCase((game, player1, player2) =>
        {
            PlayExistingHighCardBid(game, player1);
            game.Check(new(player2));
        });

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var grahamJoined = game.Join(new("Graham"));
            var knuthJoined = game.Join(new("Knuth"));
            game.StartFirstDeal();

            // act
            var exception = Record.Exception(() =>
                act(game, grahamJoined.Player.Id, knuthJoined.Player.Id));

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void CannotMakeMoveWhenGameNotStartedTest()
    {
        TestCase((game, player1, _) => PlayExistingHighCardBid(game, player1));
        TestCase((game, _, player2) => game.Check(new(player2)));

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var grahamJoined = game.Join(new("Graham"));
            var knuthJoined = game.Join(new("Knuth"));

            // act
            Assert.Throws<GameNotStartedException>(() =>
                act(game, grahamJoined.Player.Id, knuthJoined.Player.Id));
        }
    }

    [Fact]
    public void CannotMakeMoveWhenGameIsOverTest()
    {
        TestCase((game, player1, _) => PlayExistingHighCardBid(game, player1));
        TestCase((game, _, player2) => game.Check(new(player2)));

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var grahamJoined = game.Join(new("Graham"));
            var knuthJoined = game.Join(new("Knuth"));
            game.StartFirstDeal();
            GameOver(game, grahamJoined.Player, knuthJoined.Player);

            // act, assert
            Assert.Throws<GameOverException>(() =>
                act(game, grahamJoined.Player.Id, knuthJoined.Player.Id));

            static void GameOver(Game game, GamePlayer biddingPlayer, GamePlayer checkingPlayer)
            {
                var howManyTimeTheSamePlayerLostDeal = 5;
                for (int i = 0; i < howManyTimeTheSamePlayerLostDeal; i++)
                    LostByBiddingPlayer(game, biddingPlayer, checkingPlayer);
            }

            static void LostByBiddingPlayer(Game game, GamePlayer biddingPlayer, GamePlayer checkingPlayer)
            {
                PlayNotExistingLowStraightBid(game, biddingPlayer.Id);
                game.Check(new(checkingPlayer.Id));
            }
        }
    }
}