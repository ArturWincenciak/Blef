using Blef.Modules.Games.Domain.Exceptions;
using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.BidFactory;
using static Blef.Modules.Games.Domain.Tests.Extensions.DealFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class DealMoveOrderTests
{
    private Deal _deal = null!;
    private DealPlayer? _player1;
    private DealPlayer? _player2;
    private DealPlayer? _player3;
    private DealPlayer? _player4;

    [Fact]
    public void FirstPlayerCanMakeFirstMoveTest()
    {
        // arrange
        (_deal, _player1, _player2) = GivenDealWithTwoPlayers();

        // act
        var exception = Record.Exception(() =>
            PlayHighCardBid(_deal, _player1, FaceCard.Nine));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveTest()
    {
        // arrange
        (_deal, _player1, _player2) = GivenDealWithTwoPlayers();

        // act, assert
        Assert.Throws<ThatIsNotThisPlayerTurnNowException>(() =>
            PlayHighCardBid(_deal, _player2, FaceCard.Nine));
    }

    [Fact]
    public void SecondPlayerCanMakeSecondMoveTest()
    {
        Test(() => PlayHighCardBid(_deal, _player2!, FaceCard.Ten));
        Test(() => WithCheck(_deal, _player2!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2) = GivenDealWithTwoPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void FirstPlayerCannotMakeSecondMoveTest()
    {
        Test(() => PlayHighCardBid(_deal, _player1!, FaceCard.Ten));
        Test(() => WithCheck(_deal, _player1!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2) = GivenDealWithTwoPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void FirstPlayerCanMakeFirstMoveInSecondRoundTest()
    {
        Test(() => PlayHighCardBid(_deal, _player1!, FaceCard.Jack));
        Test(() => WithCheck(_deal, _player1!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2) = GivenDealWithTwoPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void SecondPlayerCannotMakeFirstMoveInSecondRoundTest()
    {
        Test(() => PlayHighCardBid(_deal, _player2!, FaceCard.Jack));
        Test(() => WithCheck(_deal, _player2!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2) = GivenDealWithTwoPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void SecondPlayerCanMakeSecondMoveInSecondRoundTest()
    {
        Test(() => PlayHighCardBid(_deal, _player2!, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player2!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2) = GivenDealWithTwoPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);
            PlayHighCardBid(_deal, _player1, FaceCard.Jack);

            // act
            var exception = Record.Exception(act);

            // assert
            Assert.Null(exception);
        }
    }

    [Fact]
    public void FirstPlayerCannotMakeSecondMoveInSecondRoundTest()
    {
        Test(() => PlayHighCardBid(_deal, _player1!, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player1!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2) = GivenDealWithTwoPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);
            PlayHighCardBid(_deal, _player1, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void GivenFourPlayersWithManyBidsInRightOrderTest()
    {
        // arrange
        (_deal, _player1, _player2, _player3, _player4) = GivenDealWithFourPlayers();

        // act
        var exception = Record.Exception(() =>
        {
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);
            PlayHighCardBid(_deal, _player3, FaceCard.Jack);
            PlayHighCardBid(_deal, _player4, FaceCard.Queen);
            PlayHighCardBid(_deal, _player1, FaceCard.King);
            PlayHighCardBid(_deal, _player2, FaceCard.Ace);
            PlayPairBid(_deal, _player3, FaceCard.Nine);
            PlayPairBid(_deal, _player4, FaceCard.Ten);
            PlayPairBid(_deal, _player1, FaceCard.Jack);
            PlayPairBid(_deal, _player2, FaceCard.Queen);
            PlayPairBid(_deal, _player3, FaceCard.King);
            PlayPairBid(_deal, _player4, FaceCard.Ace);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void FirstPlayerCannotMakeFourthMoveTest()
    {
        Test(() => PlayHighCardBid(_deal, _player1!, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player1!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2, _player3, _player4) = GivenDealWithFourPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);
            PlayHighCardBid(_deal, _player3, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void SecondPlayerCannotMakeFourthMoveTest()
    {
        Test(() => PlayHighCardBid(_deal, _player2!, FaceCard.Queen));
        Test(() => WithCheck(_deal, _player2!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2, _player3, _player4) = GivenDealWithFourPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);
            PlayHighCardBid(_deal, _player3, FaceCard.Jack);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    [Fact]
    public void FourthPlayerCannotMakeSecondMoveInSecondRoundTest()
    {
        Test(() => PlayPairBid(_deal, _player4!, FaceCard.Nine));
        Test(() => WithCheck(_deal, _player4!));

        void Test(Action act)
        {
            // arrange
            (_deal, _player1, _player2, _player3, _player4) = GivenDealWithFourPlayers();
            PlayHighCardBid(_deal, _player1, FaceCard.Nine);
            PlayHighCardBid(_deal, _player2, FaceCard.Ten);
            PlayHighCardBid(_deal, _player3, FaceCard.Jack);
            PlayHighCardBid(_deal, _player4, FaceCard.Queen);
            PlayHighCardBid(_deal, _player1, FaceCard.Ace);

            // act, assert
            Assert.Throws<ThatIsNotThisPlayerTurnNowException>(act);
        }
    }

    private static void WithCheck(Deal deal, DealPlayer byPlayer) =>
        deal.Check(new(byPlayer.Player));
}