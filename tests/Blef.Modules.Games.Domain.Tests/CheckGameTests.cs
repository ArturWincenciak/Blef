using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Shared.Abstractions.Events;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.PokerHandFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;

namespace Blef.Modules.Games.Domain.Tests;

 // TODO: DONE: add test for checking in first deal in second move (by second player)
 // TODO: add test for checking in first deal in third move (by first player)
 // TODO: add test for checking in second deal in second move (by second player)
 // TODO: add test for last check in last deal by second player (game over)
 // TODO: add test for last check in last deal by first player (game over)

public class CheckGameTests
{
    [Fact]
    public void Given_CheckInFirstDeal_SecondMoveBySecondPlayer_Then_CheckingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        var biddingPlayer = firstPlayerJoined.Player.Id;
        var checkingPlayer = secondPlayerJoined.Player.Id;
        PlayHighCardBid(game, biddingPlayer, FaceCard.Ace);

        // act
        var actualEvents = game.Check(new(checkingPlayer));

        // assert
        AssertCheckPlaced(game.Id, new DealNumber(1), checkingPlayer, checkingPlayer, actualEvents);
        AssertDealStarted(game.Id, new[]{biddingPlayer, checkingPlayer}, actualEvents);
    }

    [Fact]
    public void Given_CheckInFirstDeal_SecondMoveBySecondPlayer_Then_BiddingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        var biddingPlayer = firstPlayerJoined.Player.Id;
        var checkingPlayer = secondPlayerJoined.Player.Id;
        PlayHighStraightBid(game, biddingPlayer);

        // act
        var actualEvents = game.Check(new(checkingPlayer));

        // assert
        AssertCheckPlaced(game.Id, new DealNumber(1), checkingPlayer, biddingPlayer, actualEvents);
        AssertDealStarted(game.Id, new[] {biddingPlayer, checkingPlayer}, actualEvents);
    }

    private static void AssertCheckPlaced(
        GameId expectedGameId,
        DealNumber expectedDealNumber,
        PlayerId expectedCheckingPlayer,
        PlayerId expectedLooser,
        IEnumerable<IDomainEvent> actual)
    {
        var checkPlaced = actual.Single(@event => @event is CheckPlaced) as CheckPlaced;
        Assert.Equal(expectedGameId, checkPlaced!.Game);
        Assert.Equal(expectedDealNumber, checkPlaced.Deal);
        Assert.Equal(expectedCheckingPlayer, checkPlaced.CheckingPlayer.Player);
        Assert.Equal(expectedLooser, checkPlaced.LooserPlayer.Player);
    }

    private static void AssertDealStarted(
        GameId expectedGameId,
        IEnumerable<PlayerId> expectedNextDealPlayers,
        IEnumerable<IDomainEvent> actual)
    {
        var dealStarted = actual.Single(@event => @event is DealStarted) as DealStarted;
        Assert.Equal(expectedGameId, dealStarted!.Game);
        Assert.Equal(new DealNumber(2), dealStarted.Deal);

        var nextDealPlayers = dealStarted.Players.Select(dealPlayer => dealPlayer.Player);
        foreach (var expectedNextDealPlayer in expectedNextDealPlayers)
            Assert.Contains(nextDealPlayers, player => player == expectedNextDealPlayer);
    }

    private static (
        Game Game,
        GamePlayerJoined FirstPlayerJoined,
        GamePlayerJoined SecondPlayerJoined)
        GivenGameWithTwoPlayersWithFirstDeal()
    {
        var game = GivenGame();
        var firstPlayerJoined = game.Join(new("Graham"));
        var secondPlayerJoined = game.Join(new("Knuth"));
        game.StartFirstDeal();
        return (game, firstPlayerJoined, secondPlayerJoined);
    }
}