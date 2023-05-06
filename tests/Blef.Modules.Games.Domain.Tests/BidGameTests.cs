using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.PokerHandFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class BidGameTests
{
    [Fact]
    public void MakeBidInFirstDealTest()
    {
        // arrange
        var (game, firstPlayerJoined, _) = GivenGameWithTwoPlayersWithFirstDeal();
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, firstPlayerJoined.Player.Id);

        // act
        var actualBidPlaced = game.Bid(bid);

        // assert
        var expectedDealNumber = new DealNumber(1);
        AssertBidPlaced(pokerHand, firstPlayerJoined.Player.Id, game.Id, expectedDealNumber, actualBidPlaced);
    }

    [Fact]
    public void MakeBidInSecondDealTest()
    {
        // arrange
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenGameWithTwoPlayersWithFirstDeal();
        PlayFirstDeal(game, firstPlayerJoined.Player.Id, secondPlayerJoined.Player.Id);
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, firstPlayerJoined.Player.Id);

        // act
        var actualBidPlaced = game.Bid(bid);

        // assert
        var expectedDealNumber = new DealNumber(2);
        AssertBidPlaced(pokerHand, firstPlayerJoined.Player.Id, game.Id, expectedDealNumber, actualBidPlaced);

        static void PlayFirstDeal(Game game, PlayerId biddingPlayer, PlayerId checkingPlayer)
        {
            WithHighStraight(game, biddingPlayer);
            game.Check(new(checkingPlayer));
        }
    }

    private static (
        Game Game,
        GamePlayerJoined FirstPlayerJoined,
        GamePlayerJoined SecondPlayerJoined)
        GivenGameWithTwoPlayersWithFirstDeal()
    {
        var game = GivenGame();
        var grahamJoined = game.Join(new("Graham"));
        var knuthJoined = game.Join(new("Knuth"));
        game.StartFirstDeal();
        return (game, grahamJoined, knuthJoined);
    }

    private static void AssertBidPlaced(
        PokerHand expectedPokerHand,
        PlayerId expectedPlayer,
        GameId expectedGame,
        DealNumber expectedDealNumber,
        BidPlaced actual)
    {
        // assert
        Assert.Equal(expectedPokerHand, actual.PokerHand);
        Assert.Equal(expectedPlayer, actual.Player);
        Assert.Equal(expectedDealNumber, actual.Deal);
        Assert.Equal(expectedGame, actual.Game);
    }
}