using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.DealFactory;

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
        PlayHighCardBid(deal, firstBiddingPlayer, existingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(new(secondCheckingPlayer.Player));
        var expectedLoserPlayer = new LooserPlayer(secondCheckingPlayer.Player);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_SecondPlayerChecks_And_HighCardDoesNotExistOnTable_Then_TheFirstBiddingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondCheckingPlayer, _, _) = GivenDeal();
        var notExistingOnTheTableBidHighCard = FaceCard.Ace;
        PlayHighCardBid(deal, firstBiddingPlayer, notExistingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(new(secondCheckingPlayer.Player));
        var expectedLoserPlayer = new LooserPlayer(firstBiddingPlayer.Player);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_ThirdPlayerChecks_And_HighCardExistsOnTable_Then_TheThirdCheckingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondBiddingPlayer, thirdCheckingPlayer, _) = GivenDeal();
        PlayHighCardBid(deal, firstBiddingPlayer, FaceCard.Nine);
        var existingOnTheTableBidHighCard = FaceCard.Ten;
        PlayHighCardBid(deal, secondBiddingPlayer, existingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(new(thirdCheckingPlayer.Player));
        var expectedLoserPlayer = new LooserPlayer(thirdCheckingPlayer.Player);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_ThirdPlayerChecks_And_HighCardDoesNotExistOnTable_Then_TheSecondBiddingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondBiddingPlayer, thirdCheckingPlayer, _) = GivenDeal();
        PlayHighCardBid(deal, firstBiddingPlayer, FaceCard.Nine);
        var notExistingOnTheTableBidHighCard = FaceCard.King;
        PlayHighCardBid(deal, secondBiddingPlayer, notExistingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(new(thirdCheckingPlayer.Player));
        var expectedLoserPlayer = new LooserPlayer(secondBiddingPlayer.Player);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_FirstPlayerInSecondRoundChecks_And_HighCardExistsOnTable_Then_TheFirstCheckingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstCheckingPlayer, secondBiddingPlayer, thirdBiddingPlayer, fourthBiddingPlayer) = GivenDeal();
        PlayHighCardBid(deal, firstCheckingPlayer, FaceCard.Nine);
        PlayHighCardBid(deal, secondBiddingPlayer, FaceCard.Ten);
        PlayHighCardBid(deal, thirdBiddingPlayer, FaceCard.Jack);
        var existingOnTheTableBidHighCard = FaceCard.Queen;
        PlayHighCardBid(deal, fourthBiddingPlayer, existingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(new(firstCheckingPlayer.Player));
        var expectedLoserPlayer = new LooserPlayer(firstCheckingPlayer.Player);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void When_FirstPlayerInSecondRoundChecks_And_HighCardDoesNotExistsOnTable_Then_TheFourthBiddingPlayerIsTheTheLoser()
    {
        // arrange
        var (deal, firstCheckingPlayer, secondBiddingPlayer, thirdBiddingPlayer, fourthBiddingPlayer) = GivenDeal();
        PlayHighCardBid(deal, firstCheckingPlayer, FaceCard.Nine);
        PlayHighCardBid(deal, secondBiddingPlayer, FaceCard.Ten);
        PlayHighCardBid(deal, thirdBiddingPlayer, FaceCard.Jack);
        var notExistingOnTheTableBidHighCard = FaceCard.Ace;
        PlayHighCardBid(deal, fourthBiddingPlayer, notExistingOnTheTableBidHighCard);

        // act
        var actualLoserPlayer = deal.Check(new(firstCheckingPlayer.Player));
        var expectedLoserPlayer = new LooserPlayer(fourthBiddingPlayer.Player);

        // assert
        Assert.Equal(expectedLoserPlayer, actualLoserPlayer);
    }

    [Fact]
    public void CheckCannotBeCalledInFirstMoveTest()
    {
        // arrange
        var (deal, firstBiddingPlayer, _, _, _) = GivenDeal();

        // act
        Assert.Throws<NoBidToCheckException>(() => deal.Check(new(firstBiddingPlayer.Player)));
    }

    [Fact]
    public void Given_PlayerAlreadyChecked_When_TryingToCheckAgain_Then_ThrowsException()
    {
        // arrange
        var (deal, firstBiddingPlayer, secondCheckingPlayer, thirdPlayer, _) = GivenDeal();
        PlayHighCardBid(deal, firstBiddingPlayer, FaceCard.Nine);
        deal.Check(new(secondCheckingPlayer.Player));

        // assert
        Assert.Throws<InvalidOperationException>(() =>
        {
            // act
            deal.Check(new(thirdPlayer.Player));
        });
    }
}