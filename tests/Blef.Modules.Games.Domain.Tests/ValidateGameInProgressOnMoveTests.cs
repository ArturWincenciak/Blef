using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
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
        // arrange
        var game = GivenGame();
        var playerJoined1 = game.Join(new("Graham"));
        var _ = game.Join(new("Knuth"));
        game.StartFirstDeal();

        // act
        var exception = Record.Exception(() =>
            WithHighCardBid(game, playerJoined1.Player.Id, FaceCard.Ace));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotMakeMoveWhenGameNotStartedTest()
    {
        // arrange
        var game = GivenGame();
        var playerId = new PlayerId(Guid.Parse("6E86215C-30A8-4DF0-A5CF-072F700C3948"));

        // act
        Assert.Throws<GameNotStartedException>(() =>
            WithHighCardBid(game, playerId, FaceCard.Ace));
    }

    [Fact]
    public void CannotMakeMoveWhenGameIsOverTest()
    {
        // arrange
        var game = GivenGame();

        // todo: ...
    }
}