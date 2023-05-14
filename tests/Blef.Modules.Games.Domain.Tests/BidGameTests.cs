using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
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
        var (game, firstPlayerJoined, _) = GivenStartedGameWithTwoPlayers();
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
        var (game, firstPlayerJoined, secondPlayerJoined) = GivenStartedGameWithTwoPlayers();
        PlayFirstDeal(game, firstPlayerJoined.Player.Id, secondPlayerJoined.Player.Id);
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, secondPlayerJoined.Player.Id);

        // act
        var actualBidPlaced = game.Bid(bid);

        // assert
        var expectedDealNumber = new DealNumber(2);
        AssertBidPlaced(pokerHand, secondPlayerJoined.Player.Id, game.Id, expectedDealNumber, actualBidPlaced);

        static void PlayFirstDeal(Game game, PlayerId biddingPlayer, PlayerId checkingPlayer)
        {
            PlayHighStraightBid(game, biddingPlayer);
            game.Check(new CheckingPlayer(checkingPlayer));
        }
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