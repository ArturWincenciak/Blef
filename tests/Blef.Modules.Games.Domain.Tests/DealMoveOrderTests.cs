using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.DealFactory;

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
        (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();

        // act
        var exception = Record.Exception(() =>
            WithHighCardBid(_deal, _player_1, FaceCard.Nine));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveTest()
    {
        // arrange
        (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            WithHighCardBid(_deal, _player_2, FaceCard.Nine));
    }

    [Fact]
    public void SecondPlayerCanMakeSecondMoveTest()
    {
        Test(() => WithHighCardBid(_deal, _player_2, FaceCard.Ten));
        Test(() => WithCheck(_deal, _player_2));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void FirstPlayerCannotMakeSecondMoveTest()
    {
        Test(() => WithHighCardBid(_deal, _player_1, FaceCard.Ten));
        Test(() => WithCheck(_deal, _player_1));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void FirstPlayerCanMakeFirstMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_deal, _player_1, FaceCard.Jack));
        Test(() => WithCheck(_deal, _player_1));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_deal, _player_2, FaceCard.Jack));
        Test(() => WithCheck(_deal, _player_2));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void SecondPlayerCanMakeSecondMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_deal, _player_2, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player_2));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);
            WithHighCardBid(_deal, _player_1, FaceCard.Jack);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void FirstPlayerCannotMakeSecondMoveInSecondRoundTest()
    {
        Test(() => WithHighCardBid(_deal, _player_1, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player_1));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2) = GivenDealWithTwoPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);
            WithHighCardBid(_deal, _player_1, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void GivenFourPlayersWithManyBidsInRightOrderTest()
    {
        // arrange
        (_deal, _player_1, _player_2, _player_3, _player_4) = GivenDealWithFourPlayers();

        // act
        var exception = Record.Exception(() =>
        {
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);
            WithHighCardBid(_deal, _player_3, FaceCard.Jack);
            WithHighCardBid(_deal, _player_4, FaceCard.Queen);
            WithHighCardBid(_deal, _player_1, FaceCard.King);
            WithHighCardBid(_deal, _player_2, FaceCard.Ace);
            WithPairBid(_deal, _player_3, FaceCard.Nine);
            WithPairBid(_deal, _player_4, FaceCard.Ten);
            WithPairBid(_deal, _player_1, FaceCard.Jack);
            WithPairBid(_deal, _player_2, FaceCard.Queen);
            WithPairBid(_deal, _player_3, FaceCard.King);
            WithPairBid(_deal, _player_4, FaceCard.Ace);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void FirstPlayerCannotMakeFourthMoveTest()
    {
        Test(() => WithHighCardBid(_deal, _player_1, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player_1));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2, _player_3, _player_4) = GivenDealWithFourPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);
            WithHighCardBid(_deal, _player_3, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void SecondPlayerCannotMakeFourthMoveTest()
    {
        Test(() => WithHighCardBid(_deal, _player_2, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player_2));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2, _player_3, _player_4) = GivenDealWithFourPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);
            WithHighCardBid(_deal, _player_3, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void FourthPlayerCannotMakeSecondMoveInSecondRoundTest()
    {
        Test(() => WithPairBid(_deal, _player_4, FaceCard.Nine));
        Test(() => WithCheck(_deal, _player_4));

        void Test(Action act)
        {
            // arrange
            (_deal, _player_1, _player_2, _player_3, _player_4) = GivenDealWithFourPlayers();
            WithHighCardBid(_deal, _player_1, FaceCard.Nine);
            WithHighCardBid(_deal, _player_2, FaceCard.Ten);
            WithHighCardBid(_deal, _player_3, FaceCard.Jack);
            WithHighCardBid(_deal, _player_4, FaceCard.Queen);
            WithHighCardBid(_deal, _player_1, FaceCard.Ace);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    private static void WithCheck(Deal deal, DealPlayer byPlayer) =>
        deal.Check(new(byPlayer.Player));
}