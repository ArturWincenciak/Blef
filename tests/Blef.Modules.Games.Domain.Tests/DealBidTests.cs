using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.DealFactory;


namespace Blef.Modules.Games.Domain.Tests;

public class DealBidTests
{
    [Fact]
    public void FirstPlayerCanMakeFirstBidTest()
    {
        // arrange
        var (deal, player1, player2, player3, player4) = GivenDealWithFourPlayers();

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
        var (deal, player1, player2, player3, player4) = GivenDealWithFourPlayers();

        // assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
        {
            // act
            PlayHighCardBid(deal, player1, FaceCard.Ten);
            PlayHighCardBid(deal, player2, FaceCard.Nine);
        });
    }

    [Fact]
    public void ThirdPlayerCanMakeBidThatIsNotBetterPreviousBidTest()
    {
        // arrange
        var (deal, player1, player2, player3, player4) = GivenDealWithFourPlayers();

        // assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
        {
            // act
            PlayHighCardBid(deal, player1, FaceCard.Ten);
            PlayHighCardBid(deal, player2, FaceCard.Queen);
            PlayHighCardBid(deal, player3, FaceCard.Jack);
        });
    }

    [Fact]
    public void MultipleRightBidsTest()
    {
        // arrange
        var (deal, player1, player2, player3, player4) = GivenDealWithFourPlayers();

        // assert
        var exception = Record.Exception(() =>
        {
            // act
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
            PlayHighStraightBid(deal, player1);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotBidAfterCheckTest()
    {
        // arrange
        var (deal, player1, player2, player3, player4) = GivenDealWithFourPlayers();
        PlayHighCardBid(deal, player1, FaceCard.Nine);
        deal.Check(new(player2.Player));

        // assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            // act
            PlayHighCardBid(deal, player3, FaceCard.Ten);
        });
    }
}