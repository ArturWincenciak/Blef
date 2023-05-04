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
            WithHighCardBid(deal, player1, FaceCard.Nine));

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
            WithHighCardBid(deal, player1, FaceCard.Ten);
            WithHighCardBid(deal, player2, FaceCard.Nine);
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
            WithHighCardBid(deal, player1, FaceCard.Ten);
            WithHighCardBid(deal, player2, FaceCard.Queen);
            WithHighCardBid(deal, player3, FaceCard.Jack);
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
            WithHighCardBid(deal, player1, FaceCard.Nine);
            WithHighCardBid(deal, player2, FaceCard.Ten);
            WithHighCardBid(deal, player3, FaceCard.Jack);
            WithHighCardBid(deal, player4, FaceCard.Queen);
            WithHighCardBid(deal, player1, FaceCard.King);
            WithHighCardBid(deal, player2, FaceCard.Ace);
            WithPairBid(deal, player3, FaceCard.Nine);
            WithPairBid(deal, player4, FaceCard.Ten);
            WithPairBid(deal, player1, FaceCard.Jack);
            WithPairBid(deal, player2, FaceCard.Queen);
            WithPairBid(deal, player3, FaceCard.King);
            WithPairBid(deal, player4, FaceCard.Ace);
            WithTwoPairsBid(deal, player1, FaceCard.Nine, FaceCard.Ten);
            WithTwoPairsBid(deal, player2, FaceCard.Jack, FaceCard.Queen);
            WithTwoPairsBid(deal, player3, FaceCard.King, FaceCard.Ace);
            WithLowStraight(deal, player4);
            WithHighStraight(deal, player1);
        });

        // assert
        Assert.Null(exception);
    }


}