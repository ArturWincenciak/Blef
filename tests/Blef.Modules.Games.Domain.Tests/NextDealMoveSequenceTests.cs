﻿using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class NextDealMoveSequenceTests
{
    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInFirstDeal_Then_FirstMoveInSecondDealShouldMakeSecondPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, _) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));

        // act
        var exception = Record.Exception(() => { PlayHighCardBid(game, secondPlayer.Player.Id, FaceCard.Ace); });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInFirstDeal_Then_FirstMoveInSecondDealShouldNotMakeFirstPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, _) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(game, firstPlayer.Player.Id, FaceCard.Ace));
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInFirstDeal_Then_FirstMoveInSecondDealShouldNotMakeThirdPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(game, thirdPlayer.Player.Id, FaceCard.Ace));
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInSecondDeal_Then_FirstMoveInThirdDealShouldMakeThirdPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, secondPlayer.Player.Id);
        game.Check(new(thirdPlayer.Player.Id));

        // act
        var exception = Record.Exception(() => { PlayHighCardBid(game, thirdPlayer.Player.Id, FaceCard.Ace); });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInSecondDeal_Then_FirstMoveInThirdDealShouldNotMakeFirstPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, secondPlayer.Player.Id);
        game.Check(new(thirdPlayer.Player.Id));

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(game, firstPlayer.Player.Id, FaceCard.Ace));
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInSecondDeal_Then_FirstMoveInThirdDealShouldNotMakeSecondPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, secondPlayer.Player.Id);
        game.Check(new(thirdPlayer.Player.Id));

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(game, secondPlayer.Player.Id, FaceCard.Ace));
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInThirdDeal_Then_FirstMoveInFourthDealShouldMakeFirstPlayer()
    {
        // arrange
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, secondPlayer.Player.Id);
        game.Check(new(thirdPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, thirdPlayer.Player.Id);
        game.Check(new(firstPlayer.Player.Id));

        // act
        var exception = Record.Exception(() => { PlayHighCardBid(game, firstPlayer.Player.Id, FaceCard.Ace); });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInThirdDeal_Then_FirstMoveInFourthDealShouldNotMakeSecondPlayer()
    {
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, secondPlayer.Player.Id);
        game.Check(new(thirdPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, thirdPlayer.Player.Id);
        game.Check(new(firstPlayer.Player.Id));

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(game, secondPlayer.Player.Id, FaceCard.Ace));
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_CheckInThirdDeal_Then_FirstMoveInFourthDealShouldNotMakeThirdPlayer()
    {
        var (game, firstPlayer, secondPlayer, thirdPlayer) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, firstPlayer.Player.Id);
        game.Check(new(secondPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, secondPlayer.Player.Id);
        game.Check(new(thirdPlayer.Player.Id));
        PlayNotExistingLowStraightBid(game, thirdPlayer.Player.Id);
        game.Check(new(firstPlayer.Player.Id));

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(game, thirdPlayer.Player.Id, FaceCard.Ace));
    }
}