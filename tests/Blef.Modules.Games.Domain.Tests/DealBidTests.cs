using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealBidTests
{
    [Fact]
    public void FirstPlayerCanMakeFirstBidTest()
    {
        // arrange
        var (deal, player1, player2, player3) = GivenDealWithThreePlayers();

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
        var (deal, player1, player2, player3) = GivenDealWithThreePlayers();

        // assert
        Assert.Throws<BidIsNotHigherThenLastOneException>(() =>
        {
            // act
            WithHighCardBid(deal, player1, FaceCard.Ten);
            WithHighCardBid(deal, player2, FaceCard.Nine);
        });
    }

    private static (
        Deal Deal,
        DealPlayer First, DealPlayer Second, DealPlayer Third) GivenDealWithThreePlayers()
    {
        var dealNumber = new DealNumber(1);
        var gameGuid = Guid.Parse("F0C56541-DFDE-45B5-9A6F-FBD8CBE127D1");
        var gameId = new GameId(gameGuid);
        var dealId = new DealId(gameId, dealNumber);
        var playerGuid_1 = Guid.Parse("54C03C56-A602-43E4-B922-CF17456FDA55");
        var playerGuid_2 = Guid.Parse("DBE9B64F-0EC9-4A4F-A357-A482A09F3567");
        var playerGuid_3 = Guid.Parse("D4C01AE4-C790-41EB-BF9E-DA2CC05D7A61");
        var playerHand_1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand_2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        var playerHand_3 = new Hand(new Card[] {new(FaceCard.Queen, Suit.Diamonds)});
        var player_1 = new DealPlayer(new(playerGuid_1), playerHand_1);
        var player_2 = new DealPlayer(new(playerGuid_2), playerHand_2);
        var player_3 = new DealPlayer(new(playerGuid_3), playerHand_3);
        var players = new[] {player_1, player_2, player_3};
        var moveSequence = new MoveSequence(new[]
        {
            new Move(player_1.PlayerId, Order.Create(1)),
            new Move(player_2.PlayerId, Order.Create(2)),
            new Move(player_3.PlayerId, Order.Create(3))
        });
        var deal = new Deal(dealId, new(new(players), moveSequence));

        return (deal, player_1, player_2, player_3);
    }

    private void WithHighCardBid(Deal deal, DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = PokerHandFactory.GivenHighCard(faceCard);
        var bid = new Bid(pokerHand, byPlayer.PlayerId);
        deal.Bid(bid);
    }
}