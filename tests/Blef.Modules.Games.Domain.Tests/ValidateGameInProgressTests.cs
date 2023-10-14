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
            game.Check(new CheckingPlayer(player2));
        });
        return;

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var grahamJoined = game.Join(new PlayerNick("Graham"));
            var knuthJoined = game.Join(new PlayerNick("Knuth"));
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
        TestCase((game, _, player2) => game.Check(new CheckingPlayer(player2)));
        return;

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var grahamJoined = game.Join(new PlayerNick("Graham"));
            var knuthJoined = game.Join(new PlayerNick("Knuth"));

            // act
            Assert.Throws<GameNotStartedException>(() =>
                act(game, grahamJoined.Player.Id, knuthJoined.Player.Id));
        }
    }

    [Fact]
    public void CannotMakeMoveWhenGameIsOverTest()
    {
        TestCase((game, player1, _) => PlayExistingHighCardBid(game, player1));
        TestCase((game, _, player2) => game.Check(new CheckingPlayer(player2)));
        return;

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var grahamJoined = game.Join(new PlayerNick("Graham"));
            var knuthJoined = game.Join(new PlayerNick("Knuth"));
            game.StartFirstDeal();
            GameOver(game, grahamJoined.Player, knuthJoined.Player);

            // act, assert
            Assert.Throws<GameOverException>(() =>
                act(game, grahamJoined.Player.Id, knuthJoined.Player.Id));
            return;

            static void GameOver(Game game, GamePlayer graham, GamePlayer knuth)
            {
                // first deal lost by graham
                PlayNotExistingLowStraightBid(game, graham.Id);
                game.Check(new CheckingPlayer(knuth.Id));

                // second deal lost by graham
                PlayHighCardBid(game, knuth.Id, FaceCard.Nine);
                PlayNotExistingLowStraightBid(game, graham.Id);
                game.Check(new CheckingPlayer(knuth.Id));

                // third deal lost by graham
                PlayHighCardBid(game, knuth.Id, FaceCard.Nine);
                PlayNotExistingLowStraightBid(game, graham.Id);
                game.Check(new CheckingPlayer(knuth.Id));

                // fourth deal lost by graham
                PlayHighCardBid(game, knuth.Id, FaceCard.Nine);
                PlayNotExistingLowStraightBid(game, graham.Id);
                game.Check(new CheckingPlayer(knuth.Id));

                // fifth deal lost by graham
                PlayHighCardBid(game, knuth.Id, FaceCard.Nine);
                PlayNotExistingLowStraightBid(game, graham.Id);
                game.Check(new CheckingPlayer(knuth.Id));
            }
        }
    }
}