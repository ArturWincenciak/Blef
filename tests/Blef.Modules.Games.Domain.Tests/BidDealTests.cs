using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.DealFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class BidDealTests
{
    [Fact]
    public void FirstPlayerCanMakeFirstBidTest()
    {
        // arrange
        var (deal, player1, _, _, _) = GivenDealWithFourPlayers();

        // act
        var exception = Record.Exception(() =>
            PlayHighCardBid(deal, player1, FaceCard.Nine));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void SecondPlayerCanMakeBidThatIsNotBetterPreviousBidTest()
    {
        // arrange
        var (deal, player1, player2, _, _) = GivenDealWithFourPlayers();
        PlayHighCardBid(deal, player1, FaceCard.Ten);

        // act, assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
            PlayHighCardBid(deal, player2, FaceCard.Nine));
    }

    [Fact]
    public void ThirdPlayerCanMakeBidThatIsNotBetterPreviousBidTest()
    {
        // arrange
        var (deal, player1, player2, player3, _) = GivenDealWithFourPlayers();
        PlayHighCardBid(deal, player1, FaceCard.Ten);
        PlayHighCardBid(deal, player2, FaceCard.Queen);

        // assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
            PlayHighCardBid(deal, player3, FaceCard.Jack));
    }

    [Fact]
    public void MultipleRightBidsTest()
    {
        // arrange
        var (deal, player1, player2, player3, player4) = GivenDealWithFourPlayers();
        PlayHighCardBid(deal, player1, FaceCard.Nine);
        PlayHighCardBid(deal, player2, FaceCard.Ten);
        PlayHighCardBid(deal, player3, FaceCard.Jack);
        PlayHighCardBid(deal, player4, FaceCard.Queen);
        PlayHighCardBid(deal, player1, FaceCard.King);
        PlayHighCardBid(deal, player2, FaceCard.Ace);
        PlayPairBid(deal, player3, FaceCard.Nine);
        PlayPairBid(deal, player4, FaceCard.Ten);
        PlayPairBid(deal, player1, FaceCard.Jack);
        PlayPairBid(deal, player2, FaceCard.Queen);
        PlayPairBid(deal, player3, FaceCard.King);
        PlayPairBid(deal, player4, FaceCard.Ace);
        PlayTwoPairsBid(deal, player1, FaceCard.Nine, FaceCard.Ten);
        PlayTwoPairsBid(deal, player2, FaceCard.Jack, FaceCard.Queen);
        PlayTwoPairsBid(deal, player3, FaceCard.King, FaceCard.Ace);
        PlayLowStraightBid(deal, player4);

        // act, assert
        var exception = Record.Exception(() =>
            PlayHighStraightBid(deal, player1));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotBidAfterCheckTest()
    {
        // arrange
        var (deal, player1, player2, player3, _) = GivenDealWithFourPlayers();
        PlayHighCardBid(deal, player1, FaceCard.Nine);
        deal.Check(new CheckingPlayer(player2.Player));

        // act, assert
        Assert.Throws<InvalidOperationException>(() =>
            PlayHighCardBid(deal, player3, FaceCard.Ten));
    }
}