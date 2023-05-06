using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class ValidateGameInProgressOnMoveTests
{
    [Fact]
    public void CanMakeMoveWhenGameInProgressTest()
    {
        TestCase((game, player1, _) => PlayHighCardBid(game, player1, FaceCard.Ace));
        TestCase((game, player1, player2) =>
        {
            PlayHighCardBid(game, player1, FaceCard.Ace);
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
        TestCase((game, player1, _) => PlayHighCardBid(game, player1, FaceCard.Ace));
        TestCase((game, _, player2) => game.Check(new(player2)));

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var playerId = new PlayerId(Guid.Parse("6E86215C-30A8-4DF0-A5CF-072F700C3948"));

            // act
            Assert.Throws<GameNotStartedException>(() =>
                PlayHighCardBid(game, playerId, FaceCard.Ace));
        }
    }

    [Fact]
    public void CannotMakeMoveWhenGameIsOverTest()
    {
        TestCase((game, player1, _) => PlayHighCardBid(game, player1, FaceCard.Ace));
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

            static void GameOver(Game game, GamePlayer gamePlayer1, GamePlayer gamePlayer2)
            {
                var howManyTimeTheSamePlayerLostDeal = 5;
                for (int i = 0; i < howManyTimeTheSamePlayerLostDeal; i++)
                    LostBiddingPlayer(game, gamePlayer1, gamePlayer2);
            }

            static void LostBiddingPlayer(Game game, GamePlayer gamePlayer1, GamePlayer gamePlayer2)
            {
                PlayLowStraightBid(game, gamePlayer1.Id);
                game.Check(new(gamePlayer2.Id));
            }
        }
    }
}