using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.AssertExtension;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class PlayerLostGameTests
{
    [Fact]
    public void Given_GameWithThreePlayers_When_Check_Then_FirstPlayerLost()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined) = GivenStartedGameWithThreePlayers();

        // first deal lost by graham
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // second deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // third deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // fourth deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // fifth deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);

        // act
        var actual = game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = knuthJoined.Player.Id;
        var expectedLooser = grahamJoined.Player.Id;
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(5),
            expectedCheckingPlayer, expectedLooser,
            actual);
        var expectedNextDealPlayers = new[] {knuthJoined.Player.Id, planckJoined.Player.Id};
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(6),
            expectedNextDealPlayers, actual);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_Check_Then_SecondPlayerLost()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined) = GivenStartedGameWithThreePlayers();

        // first deal lost by knuth
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // second deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // third deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // fourth deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // fifth deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);

        // act
        var actual = game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = planckJoined.Player.Id;
        var expectedLooser = knuthJoined.Player.Id;
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(5),
            expectedCheckingPlayer, expectedLooser,
            actual);
        var expectedNextDealPlayers = new[] {grahamJoined.Player.Id, planckJoined.Player.Id};
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(6),
            expectedNextDealPlayers, actual);
    }

    [Fact]
    public void Given_GameWithThreePlayers_When_Check_Then_ThirdPlayerLost()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined) = GivenStartedGameWithThreePlayers();

        // first deal lost by planck
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new CheckingPlayer(grahamJoined.Player.Id));

        // second deal lost by planck
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new CheckingPlayer(grahamJoined.Player.Id));

        // third deal lost by planck
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new CheckingPlayer(grahamJoined.Player.Id));

        // fourth deal lost by planck
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);
        game.Check(new CheckingPlayer(grahamJoined.Player.Id));

        // fifth deal lost by planck
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Ten);
        PlayNotExistingLowStraightBid(game, planckJoined.Player.Id);

        // act
        var actual = game.Check(new CheckingPlayer(grahamJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = grahamJoined.Player.Id;
        var expectedLooser = planckJoined.Player.Id;
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(5),
            expectedCheckingPlayer, expectedLooser,
            actual);
        var expectedNextDealPlayers = new[] {grahamJoined.Player.Id, knuthJoined.Player.Id};
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(6),
            expectedNextDealPlayers, actual);
    }

    [Fact]
    public void Given_GameWithFourPlayers_When_Check_Then_FirstPlayerLost()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined, riemannJoined) = GivenStartedGameWithFourPlayers();

        // first deal lost by graham
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // second deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // third deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // fourth deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);
        game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // fifth deal lost by graham
        PlayHighCardBid(game, knuthJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, grahamJoined.Player.Id);

        // act
        var actual = game.Check(new CheckingPlayer(knuthJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = knuthJoined.Player.Id;
        var expectedLooser = grahamJoined.Player.Id;
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(5),
            expectedCheckingPlayer, expectedLooser,
            actual);
        var expectedNextDealPlayers = new[]
        {
            knuthJoined.Player.Id, planckJoined.Player.Id, riemannJoined.Player.Id
        };
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(6),
            expectedNextDealPlayers, actual);
    }

    [Fact]
    public void Given_GameWithFourPlayers_When_Check_Then_SecondPlayerLost()
    {
        // arrange
        var (game, grahamJoined, knuthJoined, planckJoined, riemannJoined) = GivenStartedGameWithFourPlayers();

        // first deal lost by knuth
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Nine);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // second deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // third deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // fourth deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);
        game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // fifth deal lost by knuth
        PlayHighCardBid(game, planckJoined.Player.Id, FaceCard.Nine);
        PlayHighCardBid(game, riemannJoined.Player.Id, FaceCard.Ten);
        PlayHighCardBid(game, grahamJoined.Player.Id, FaceCard.Jack);
        PlayNotExistingLowStraightBid(game, knuthJoined.Player.Id);

        // act
        var actual = game.Check(new CheckingPlayer(planckJoined.Player.Id));

        // assert
        var expectedCheckingPlayer = planckJoined.Player.Id;
        var expectedLooser = knuthJoined.Player.Id;
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(5),
            expectedCheckingPlayer, expectedLooser,
            actual);

        var expectedNextDealPlayers = new[]
        {
            riemannJoined.Player.Id, planckJoined.Player.Id, grahamJoined.Player.Id
        };
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(6),
            expectedNextDealPlayers, actual);
    }
}