using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.AssertExtension;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.PokerHandFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class LooserTests
{
    [Fact]
    public void Given_GameWithThreePlayers_When_Check_Then_FirstPlayerLostTest()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined) = GivenStartedGameWithThreePlayers();
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new(knuthJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new(knuthJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new(knuthJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new(knuthJoined.Player.Id));
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);

        // act
        var actual = game.Check(new(knuthJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = knuthJoined.Player.Id;
        var expectedLooser = grahamJoined.Player.Id;
        AssertCheckPlaced(game.Id, new DealNumber(5), expectedCheckingPlayer, expectedLooser, actual);
        var expectedNextDealPlayers = new[] {knuthJoined.Player.Id, planckJoined.Player.Id};
        AssertDealStarted(game.Id, new DealNumber(6), expectedNextDealPlayers, actual);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_Check_Then_SecondPlayerLostTest()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined) = GivenStartedGameWithThreePlayers();
        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new(planckJoined.Player.Id));
        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new(planckJoined.Player.Id));
        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new(planckJoined.Player.Id));
        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new(planckJoined.Player.Id));
        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);

        // act
        var actual = game.Check(new(planckJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = planckJoined.Player.Id;
        var expectedLooser = knuthJoined.Player.Id;
        AssertCheckPlaced(game.Id, new DealNumber(5), expectedCheckingPlayer, expectedLooser, actual);
        var expectedNextDealPlayers = new[] {grahamJoined.Player.Id, planckJoined.Player.Id};
        AssertDealStarted(game.Id, new DealNumber(6), expectedNextDealPlayers, actual);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_Check_Then_ThirdPlayerLostTest()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined) = GivenStartedGameWithThreePlayers();
        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayExistingPairBid(game, knuthJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new(grahamJoined.Player.Id));

        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayExistingPairBid(game, knuthJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new(grahamJoined.Player.Id));

        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayExistingPairBid(game, knuthJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new(grahamJoined.Player.Id));

        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayExistingPairBid(game, knuthJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new(grahamJoined.Player.Id));

        PlayExistingHighCardBid(game, grahamJoined.Player.Id);
        PlayExistingPairBid(game, knuthJoined.Player.Id);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);

        // act
        var actual = game.Check(new(grahamJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = grahamJoined.Player.Id;
        var expectedLooser = planckJoined.Player.Id;
        AssertCheckPlaced(game.Id, new DealNumber(5), expectedCheckingPlayer, expectedLooser, actual);
        var expectedNextDealPlayers = new[] {grahamJoined.Player.Id, knuthJoined.Player.Id};
        AssertDealStarted(game.Id, new DealNumber(6), expectedNextDealPlayers, actual);
    }

    // todo: add tests for looser with 4 players
}