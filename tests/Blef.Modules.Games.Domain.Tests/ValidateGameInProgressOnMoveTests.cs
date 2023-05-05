using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class ValidateGameInProgressOnMoveTests
{
    [Fact]
    public void CanMakeMoveWhenGameInProgressTest()
    {
        TestCase((game, player1, _) => WithHighCardBid(game, player1, FaceCard.Ace));
        TestCase((game, player1, player2) =>
        {
            WithHighCardBid(game, player1, FaceCard.Ace);
            game.Check(new(player2));
        });

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var playerJoined1 = game.Join(new("Graham"));
            var playerJoined2 = game.Join(new("Knuth"));
            game.StartFirstDeal();

            // act
            var exception = Record.Exception(() =>
                act(game, playerJoined1.Player.Id, playerJoined2.Player.Id));

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void CannotMakeMoveWhenGameNotStartedTest()
    {
        TestCase((game, player1, _) => WithHighCardBid(game, player1, FaceCard.Ace));
        TestCase((game, _, player2) => game.Check(new(player2)));

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var playerId = new PlayerId(Guid.Parse("6E86215C-30A8-4DF0-A5CF-072F700C3948"));

            // act
            Assert.Throws<GameNotStartedException>(() =>
                WithHighCardBid(game, playerId, FaceCard.Ace));
        }
    }

    [Fact]
    public void CannotMakeMoveWhenGameIsOverTest()
    {
        TestCase((game, player1, _) => WithHighCardBid(game, player1, FaceCard.Ace));
        TestCase((game, _, player2) => game.Check(new(player2)));

        void TestCase(Action<Game, PlayerId, PlayerId> act)
        {
            // arrange
            var game = GivenGame();
            var playerJoined1 = game.Join(new("Graham"));
            var playerJoined2 = game.Join(new("Knuth"));
            game.StartFirstDeal();
            GameOver(game, playerJoined1.Player, playerJoined2.Player);

            // act, assert
            Assert.Throws<GameOverException>(() =>
                act(game, playerJoined1.Player.Id, playerJoined2.Player.Id));

            static void GameOver(Game game, GamePlayer gamePlayer1, GamePlayer gamePlayer2)
            {
                var howManyTimeTheSamePlayerLostDeal = 5;
                for (int i = 0; i < howManyTimeTheSamePlayerLostDeal; i++)
                    LostBiddingPlayer(game, gamePlayer1, gamePlayer2);
            }

            static void LostBiddingPlayer(Game game, GamePlayer gamePlayer1, GamePlayer gamePlayer2)
            {
                WithLowStraight(game, gamePlayer1.Id);
                game.Check(new(gamePlayer2.Id));
            }
        }
    }
}