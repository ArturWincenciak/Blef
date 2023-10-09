using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.AssertExtension;

namespace Blef.Modules.Games.Domain.Tests;

public class CheckGameTests
{
    [Fact]
    public void When_CheckInFirstDeal_Then_CheckingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenStartedGameWithTwoPlayers();
        var biddingPlayer = firstPlayerJoined.Player.Id;
        var checkingPlayer = secondPlayerJoined.Player.Id;
        PlayExistingHighCardBid(game, biddingPlayer);

        // act
        var actualEvents = game.Check(new CheckingPlayer(checkingPlayer));

        // assert
        AssertCheckPlaced(game.Id, expectedDealNumber: new DealNumber(1), checkingPlayer, checkingPlayer, actualEvents);
        AssertDealStarted(game.Id, expectedDealNumber: new DealNumber(2),
            expectedNextDealPlayers: new[] {biddingPlayer, checkingPlayer}, actualEvents);
    }

    [Fact]
    public void When_CheckInFirstDeal_Then_BiddingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenStartedGameWithTwoPlayers();
        var biddingPlayer = firstPlayerJoined.Player.Id;
        var checkingPlayer = secondPlayerJoined.Player.Id;
        PlayNotExistingLowStraightBid(game, biddingPlayer);

        // act
        var actualEvents = game.Check(new CheckingPlayer(checkingPlayer));

        // assert
        AssertCheckPlaced(game.Id, expectedDealNumber: new DealNumber(1), checkingPlayer, biddingPlayer, actualEvents);
        AssertDealStarted(game.Id, expectedDealNumber: new DealNumber(2),
            expectedNextDealPlayers: new[] {biddingPlayer, checkingPlayer}, actualEvents);
    }

    [Fact]
    public void When_CheckInSecondDeal_Then_CheckingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenStartedGameWithTwoPlayers();
        var firstPlayer = firstPlayerJoined.Player.Id;
        var secondPlayer = secondPlayerJoined.Player.Id;
        PlayExistingHighCardBid(game, firstPlayer);
        game.Check(new CheckingPlayer(secondPlayer));
        PlayExistingHighCardBid(game, firstPlayer);
        PlayExistingPairBid(game, secondPlayer);

        // act
        var actualEvents = game.Check(new CheckingPlayer(firstPlayer));

        // assert
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(2),
            firstPlayer, firstPlayer, actualEvents);
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(3),
            expectedNextDealPlayers: new[] {firstPlayer, secondPlayer}, actualEvents);
    }

    [Fact]
    public void When_CheckInSecondDeal_Then_BiddingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenStartedGameWithTwoPlayers();
        var firstPlayer = firstPlayerJoined.Player.Id;
        var secondPlayer = secondPlayerJoined.Player.Id;
        PlayExistingHighCardBid(game, firstPlayer);
        game.Check(new CheckingPlayer(secondPlayer));
        PlayExistingHighCardBid(game, firstPlayer);
        PlayNotExistingLowStraightBid(game, secondPlayer);

        // act
        var actualEvents = game.Check(new CheckingPlayer(firstPlayer));

        // assert
        AssertCheckPlaced(game.Id,
            expectedDealNumber: new DealNumber(2),
            firstPlayer, secondPlayer,
            actualEvents);
        AssertDealStarted(game.Id,
            expectedDealNumber: new DealNumber(3),
            expectedNextDealPlayers: new[] {firstPlayer, secondPlayer}, actualEvents);
    }
}