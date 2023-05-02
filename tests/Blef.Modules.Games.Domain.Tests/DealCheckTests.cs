using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using static Blef.Modules.Games.Domain.Tests.BidMaker;
using static Blef.Modules.Games.Domain.Tests.DealFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class DealCheckTests
{
    private static (Deal Deal, DealPlayer First, DealPlayer Second, DealPlayer Third, DealPlayer Fourth) GivenDeal()
    {
        var (firstPlayerHand, secondPlayerHand, thirdPlayerHand, fourthPlayerHand) =
            ((Hand First, Hand Second, Hand Third, Hand Fourth)) (
                new Hand(new Card[] {new(FaceCard.Nine, Suit.Clubs)}),
                new Hand(new Card[] {new(FaceCard.Ten, Suit.Clubs)}),
                new Hand(new Card[] {new(FaceCard.Jack, Suit.Clubs)}),
                new Hand(new Card[] {new(FaceCard.Queen, Suit.Clubs)})
            );

        return GivenDealWithFourPlayers(firstPlayerHand, secondPlayerHand, thirdPlayerHand, fourthPlayerHand);
    }

    [Fact]
    public void When_SecondPlayerChecks_And_HighCardExistsOnTable_Then_TheSecondCheckingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondCheckingPlayer, _, _) = GivenDeal();
        var existingOnTheTableBidHighCard = FaceCard.Nine;
        WithHighCardBid(deal, firstBiddingPlayer, existingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(secondCheckingPlayer.PlayerId);
        var expectedLoserPlayer = new LooserPlayer(secondCheckingPlayer.PlayerId.Id);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_SecondPlayerChecks_And_HighCardDoesNotExistOnTable_Then_TheFirstBiddingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondCheckingPlayer, _, _) = GivenDeal();
        var notExistingOnTheTableBidHighCard = FaceCard.Ace;
        WithHighCardBid(deal, firstBiddingPlayer, notExistingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(secondCheckingPlayer.PlayerId);
        var expectedLoserPlayer = new LooserPlayer(firstBiddingPlayer.PlayerId.Id);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_ThirdPlayerChecks_And_HighCardExistsOnTable_Then_TheThirdCheckingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondBiddingPlayer, thirdCheckingPlayer, _) = GivenDeal();
        WithHighCardBid(deal, firstBiddingPlayer, FaceCard.Nine);
        var existingOnTheTableBidHighCard = FaceCard.Ten;
        WithHighCardBid(deal, secondBiddingPlayer, existingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(thirdCheckingPlayer.PlayerId);
        var expectedLoserPlayer = new LooserPlayer(thirdCheckingPlayer.PlayerId.Id);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_ThirdPlayerChecks_And_HighCardDoesNotExistOnTable_Then_TheSecondBiddingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondBiddingPlayer, thirdCheckingPlayer, _) = GivenDeal();
        WithHighCardBid(deal, firstBiddingPlayer, FaceCard.Nine);
        var notExistingOnTheTableBidHighCard = FaceCard.King;
        WithHighCardBid(deal, secondBiddingPlayer, notExistingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(thirdCheckingPlayer.PlayerId);
        var expectedLoserPlayer = new LooserPlayer(secondBiddingPlayer.PlayerId.Id);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_FirstPlayerInSecondRoundChecks_And_HighCardExistsOnTable_Then_TheFirstCheckingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstCheckingPlayer, secondBiddingPlayer, thirdBiddingPlayer, fourthBiddingPlayer) = GivenDeal();
        WithHighCardBid(deal, firstCheckingPlayer, FaceCard.Nine);
        WithHighCardBid(deal, secondBiddingPlayer, FaceCard.Ten);
        WithHighCardBid(deal, thirdBiddingPlayer, FaceCard.Jack);
        var existingOnTheTableBidHighCard = FaceCard.Queen;
        WithHighCardBid(deal, fourthBiddingPlayer, existingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(firstCheckingPlayer.PlayerId);
        var expectedLoserPlayer = new LooserPlayer(firstCheckingPlayer.PlayerId.Id);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_FirstPlayerInSecondRoundChecks_And_HighCardDoesNotExistsOnTable_Then_TheFourthBiddingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstCheckingPlayer, secondBiddingPlayer, thirdBiddingPlayer, fourthBiddingPlayer) = GivenDeal();
        WithHighCardBid(deal, firstCheckingPlayer, FaceCard.Nine);
        WithHighCardBid(deal, secondBiddingPlayer, FaceCard.Ten);
        WithHighCardBid(deal, thirdBiddingPlayer, FaceCard.Jack);
        var notExistingOnTheTableBidHighCard = FaceCard.Ace;
        WithHighCardBid(deal, fourthBiddingPlayer, notExistingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(firstCheckingPlayer.PlayerId);
        var expectedLoserPlayer = new LooserPlayer(fourthBiddingPlayer.PlayerId.Id);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void CheckCannotBeCalledInFirstMoveTest()
    {
        // arrange
        var (deal, firstBiddingPlayer, _, _, _) = GivenDeal();

        // act
        Assert.Throws<NoBidToCheckException>(() => deal.Check(firstBiddingPlayer.PlayerId));
    }
}