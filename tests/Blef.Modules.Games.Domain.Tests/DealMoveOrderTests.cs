using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using static Blef.Modules.Games.Domain.Tests.PokerHandFactory;
using Action = System.Action;

namespace Blef.Modules.Games.Domain.Tests;

public class DealMoveOrderTests
{
    private Deal _deal;
    private DealPlayer _player_1;
    private DealPlayer _player_2;
    private DealPlayer _player_3;
    private DealPlayer _player_4;

    [Fact]
    public void FirstPlayerCanMakeFirstMoveTest()
    {
        // arrange
        GivenDealWithTwoPlayers();

        // act
        var exception = Record.Exception(() =>
            WithHighCardBid(_player_1, FaceCard.Nine));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveTest()
    {
        // arrange
        GivenDealWithTwoPlayers();

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            WithHighCardBid(_player_2, FaceCard.Nine));
    }

    [Fact]
    public void SecondPlayerCanMakeSecondMoveTest()
    {
        Test(() => WithHighCardBid(_player_2, FaceCard.Ten));
        Test(() => WithCheck(_player_2));

        void Test(Action act)
        {
            // arrange
            GivenDealWithTwoPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void FirstPlayerCannotMakeSecondMoveTest()
    {
        Test(() => WithHighCardBid(_player_1, FaceCard.Ten));
        Test(() => WithCheck(_player_1));

        void Test(Action act)
        {
            // arrange
            GivenDealWithTwoPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void FirstPlayerCanMakeFirstMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_player_1, FaceCard.Jack));
        Test(() => WithCheck(_player_1));

        void Test(Action act)
        {
            // arrange
            GivenDealWithTwoPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_player_2, FaceCard.Jack));
        Test(() => WithCheck(_player_2));

        void Test(Action act)
        {
            // arrange
            GivenDealWithTwoPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void SecondPlayerCanMakeSecondMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_player_2, FaceCard.Queen));
        Test(() => WithCheck(_player_2));

        void Test(Action act)
        {
            // arrange
            GivenDealWithTwoPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);
            WithHighCardBid(_player_1, FaceCard.Jack);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void FirstPlayerCannotMakeSecondMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_player_1, FaceCard.Queen));
        Test(() => WithCheck(_player_1));

        void Test(Action act)
        {
            // arrange
            GivenDealWithTwoPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);
            WithHighCardBid(_player_1, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void GivenFourPlayersWithManyBidsInRightOrderTest()
    {
        // arrange
        GivenDealWithFourPlayers();

        // act
        var exception = Record.Exception(() =>
        {
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);
            WithHighCardBid(_player_3, FaceCard.Jack);
            WithHighCardBid(_player_4, FaceCard.Queen);
            WithHighCardBid(_player_1, FaceCard.King);
            WithHighCardBid(_player_2, FaceCard.Ace);
            WithPairBid(_player_3, FaceCard.Nine);
            WithPairBid(_player_4, FaceCard.Ten);
            WithPairBid(_player_1, FaceCard.Jack);
            WithPairBid(_player_2, FaceCard.Queen);
            WithPairBid(_player_3, FaceCard.King);
            WithPairBid(_player_4, FaceCard.Ace);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void FirstPlayerCannotMakeFourthMoveTest()
    {
        Test(() => WithHighCardBid(_player_1, FaceCard.Queen));
        Test(() => WithCheck(_player_1));

        void Test(Action act)
        {
            // arrange
            GivenDealWithFourPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);
            WithHighCardBid(_player_3, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void SecondPlayerCannotMakeFourthMoveTest()
    {
        Test(() => WithHighCardBid(_player_2, FaceCard.Queen));
        Test(() => WithCheck(_player_2));

        void Test(Action act)
        {
            // arrange
            GivenDealWithFourPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);
            WithHighCardBid(_player_3, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void FourthPlayerCannotMakeSecondMoveInSecondRoundTest()
    {
        Test(() => WithPairBid(_player_4, FaceCard.Nine));
        Test(() => WithCheck(_player_4));

        void Test(Action act)
        {
            // arrange
            GivenDealWithFourPlayers();
            WithHighCardBid(_player_1, FaceCard.Nine);
            WithHighCardBid(_player_2, FaceCard.Ten);
            WithHighCardBid(_player_3, FaceCard.Jack);
            WithHighCardBid(_player_4, FaceCard.Queen);
            WithHighCardBid(_player_1, FaceCard.Ace);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    private void GivenDealWithTwoPlayers()
    {
        var dealNumber = new DealNumber(1);
        var gameGuid = Guid.Parse("B421AC19-1463-4A1A-97C8-D1E8343C012C");
        var gameId = new GameId(gameGuid);
        var dealId = new DealId(gameId, dealNumber);
        var playerGuid_1 = Guid.Parse("937A3CF3-CB70-4D00-B43C-153819BABA6B");
        var playerGuid_2 = Guid.Parse("97994058-A958-46F6-B2EF-C6C4DFF14694");
        var playerHand_1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand_2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        _player_1 = new DealPlayer(new(playerGuid_1), playerHand_1, Order.Create(1));
        _player_2 = new DealPlayer(new(playerGuid_2), playerHand_2, Order.Create(2));
        var players = new[] {_player_1, _player_2};
        _deal = new Deal(dealId, players);
    }

    private void GivenDealWithFourPlayers()
    {
        var dealNumber = new DealNumber(1);
        var gameGuid = Guid.Parse("7AEF7FEA-BF1A-4A1C-815A-980362F5C90E");
        var gameId = new GameId(gameGuid);
        var dealId = new DealId(gameId, dealNumber);
        var playerGuid_1 = Guid.Parse("5D6B8789-F67B-4E15-8E10-F2F5F1DC8D33");
        var playerGuid_2 = Guid.Parse("F9CF8347-20F8-46DD-A8B6-4E782AE1E1E5");
        var playerGuid_3 = Guid.Parse("3EBD0E98-6695-4073-B69E-42015005602E");
        var playerGuid_4 = Guid.Parse("4B4C9857-5771-4430-9580-B70955F5E2C6");
        var playerHand_1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand_2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        var playerHand_3 = new Hand(new Card[] {new(FaceCard.Queen, Suit.Spades)});
        var playerHand_4 = new Hand(new Card[] {new(FaceCard.Jack, Suit.Hearts)});
        _player_1 = new DealPlayer(new(playerGuid_1), playerHand_1, Order.Create(1));
        _player_2 = new DealPlayer(new(playerGuid_2), playerHand_2, Order.Create(2));
        _player_3 = new DealPlayer(new(playerGuid_3), playerHand_3, Order.Create(3));
        _player_4 = new DealPlayer(new(playerGuid_4), playerHand_4, Order.Create(4));
        var players = new[] {_player_1, _player_2, _player_3, _player_4};
        _deal = new Deal(dealId, players);
    }

    private void WithHighCardBid(DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenHighCard(faceCard);
        var bid = new Bid(pokerHand, byPlayer.PlayerId);
        _deal.Bid(bid);
    }

    private void WithPairBid(DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenPair(faceCard);
        var bid = new Bid(pokerHand, byPlayer.PlayerId);
        _deal.Bid(bid);
    }

    private void WithCheck(DealPlayer byPlayer) =>
        _deal.Check(byPlayer.PlayerId);
}