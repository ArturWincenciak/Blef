using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.ValidateBidPayload;

[UsesVerify]
public class ValidateBidTests
{
    private TestBuilder Arrange => new TestBuilder()
        .NewGame()
        .JoinPlayer(WhichPlayer.Conway)
        .JoinPlayer(WhichPlayer.Graham)
        .NewDeal();

    [Fact]
    public Task HighCardBidWithSuccessTest()
    {
        var results = Arrange
            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task HighCardBidWithNotValidValueTest()
    {
        var results = Arrange
            .BidHighCard(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task PairBidWithSuccessTest()
    {
        var results = Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ten)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task PairBidWithNotValidValueTest()
    {
        var results = Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task TwoPairsBidWithSuccessTest()
    {
        var results = Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.Queen)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task TwoPairsBidWithNotValidFirstValueTest()
    {
        var results = Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.Queen)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task TwoPairsBidWithNotValidSecondValueTest()
    {
        var results = Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task TwoPairsBidWithNotValidValuesTest()
    {
        var results = Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task TwoPairsBidWithTwoTheSameValuesTest()
    {
        var results = Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.Jack)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task LowStraightBidWithSuccessTest()
    {
        var results = Arrange
            .BidLowStraight(WhichPlayer.Conway)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task HighStraightBidWithSuccessTest()
    {
        var results = Arrange
            .BidHighStraight(WhichPlayer.Conway)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task ThreeOfAKindBidWithSuccessTest()
    {
        var results = Arrange
            .BidThreeOfAKind(WhichPlayer.Conway, FaceCard.King)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task ThreeOfAKindBidWithNotValidValueTest()
    {
        var results = Arrange
            .BidThreeOfAKind(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task FullHouseBidWithSuccessTest()
    {
        var results = Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.Jack)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task FullHouseBidWithNotValidFirstValueTest()
    {
        var results = Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.Jack)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task FullHouseBidWithNotValidSecondValueTest()
    {
        var results = Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task FullHouseBidWithNotValidValuesTest()
    {
        var results = Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.NotValidValue)
            .Build();

        return Verify(results);
    }

    [Fact]
    public Task FullHouseBidWithTwoTheSameValuesTest()
    {
        var results = Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.Ace)
            .Build();

        return Verify(results);
    }
}
