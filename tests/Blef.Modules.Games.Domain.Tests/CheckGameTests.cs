using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Events;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class CheckGameTests
{
    [Fact]
    public void Given_CheckInFirstDeal_SecondMoveBySecondPlayer_Then_CheckingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        var biddingPlayer = firstPlayerJoined.Player.Id;
        var checkingPlayer = secondPlayerJoined.Player.Id;
        PlayExistingHighCardBid(game, biddingPlayer);

        // act
        var actualEvents = game.Check(new(checkingPlayer));

        // assert
        AssertCheckPlaced(game.Id, new DealNumber(1), checkingPlayer, checkingPlayer, actualEvents);
        AssertDealStarted(game.Id, new DealNumber(2), new[]{biddingPlayer, checkingPlayer}, actualEvents);
    }

    [Fact]
    public void Given_CheckInFirstDeal_SecondMoveBySecondPlayer_Then_BiddingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        var biddingPlayer = firstPlayerJoined.Player.Id;
        var checkingPlayer = secondPlayerJoined.Player.Id;
        PlayTooHighPokerHand(game, biddingPlayer);

        // act
        var actualEvents = game.Check(new(checkingPlayer));

        // assert
        AssertCheckPlaced(game.Id, new DealNumber(1), checkingPlayer, biddingPlayer, actualEvents);
        AssertDealStarted(game.Id, new DealNumber(2), new[] {biddingPlayer, checkingPlayer}, actualEvents);
    }

    [Fact]
    public void Given_CheckInSecondDeal_ThirdMoveByFirstPlayer_Then_CheckingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        var firstPlayer = firstPlayerJoined.Player.Id;
        var secondPlayer = secondPlayerJoined.Player.Id;
        PlayExistingHighCardBid(game, firstPlayer);
        game.Check(new(secondPlayer));
        PlayExistingHighCardBid(game, firstPlayer);
        PlayExistingPairBid(game, secondPlayer);

        // act
        var actualEvents = game.Check(new(firstPlayer));

        // assert
        AssertCheckPlaced(game.Id, new DealNumber(2), firstPlayer, firstPlayer, actualEvents);
        AssertDealStarted(game.Id, new DealNumber(3), new[] {firstPlayer, secondPlayer}, actualEvents);
    }

    [Fact]
    public void Given_CheckInSecondDeal_ThirdMoveByFirstPlayer_Then_BiddingPlayerLoses()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        var firstPlayer = firstPlayerJoined.Player.Id;
        var secondPlayer = secondPlayerJoined.Player.Id;
        PlayExistingHighCardBid(game, firstPlayer);
        game.Check(new(secondPlayer));
        PlayExistingHighCardBid(game, firstPlayer);
        PlayTooHighPokerHand(game, secondPlayer);

        // act
        var actualEvents = game.Check(new(firstPlayer));

        // assert
        AssertCheckPlaced(game.Id, new DealNumber(2), firstPlayer, secondPlayer, actualEvents);
        AssertDealStarted(game.Id, new DealNumber(3), new[] {firstPlayer, secondPlayer}, actualEvents);
    }

    private static void PlayExistingHighCardBid(Game game, PlayerId biddingPlayer) =>
        PlayHighCardBid(game, biddingPlayer, FaceCard.Ace);

    private static void PlayExistingPairBid(Game game, PlayerId biddingPlayer) =>
        PlayPairBid(game, biddingPlayer, FaceCard.Ace);

    private static void PlayTooHighPokerHand(Game game, PlayerId biddingPlayer) =>
        PlayHighStraightBid(game, biddingPlayer);

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
        DealNumber expectedDealNumber,
        IEnumerable<PlayerId> expectedNextDealPlayers,
        IEnumerable<IDomainEvent> actual)
    {
        var dealStarted = actual.Single(@event => @event is DealStarted) as DealStarted;
        Assert.Equal(expectedGameId, dealStarted!.Game);
        Assert.Equal(expectedDealNumber, dealStarted.Deal);

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