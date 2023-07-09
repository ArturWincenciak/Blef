using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.ValidateBidPayload;

[UsesVerify]
public class ValidateBidTests
{
    private static TestBuilder Arrange => new TestBuilder()
        .NewGame()
        .JoinPlayer(WhichPlayer.Conway)
        .JoinPlayer(WhichPlayer.Graham)
        .NewDeal();

    [Fact]
    public async Task HighCardBidWithSuccessTest()
    {
        var results = await Arrange
            .BidHighCard(WhichPlayer.Conway, FaceCard.Nine)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task HighCardBidWithNotValidValueTest()
    {
        var results = await Arrange
            .BidHighCard(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task PairBidWithSuccessTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ten)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task PairBidWithNotValidValueTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task TwoPairsBidWithSuccessTest()
    {
        var results = await Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.Queen)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task TwoPairsBidWithNotValidFirstValueTest()
    {
        var results = await Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.Queen)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task TwoPairsBidWithNotValidSecondValueTest()
    {
        var results = await Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task TwoPairsBidWithNotValidValuesTest()
    {
        var results = await Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task TwoPairsBidWithTwoTheSameValuesTest()
    {
        var results = await Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.Jack)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task LowStraightBidWithSuccessTest()
    {
        var results = await Arrange
            .BidLowStraight(WhichPlayer.Conway)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task HighStraightBidWithSuccessTest()
    {
        var results = await Arrange
            .BidHighStraight(WhichPlayer.Conway)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task ThreeOfAKindBidWithSuccessTest()
    {
        var results = await Arrange
            .BidThreeOfAKind(WhichPlayer.Conway, FaceCard.King)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task ThreeOfAKindBidWithNotValidValueTest()
    {
        var results = await Arrange
            .BidThreeOfAKind(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FullHouseBidWithSuccessTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.Jack)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FullHouseBidWithNotValidFirstValueTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.Jack)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FullHouseBidWithNotValidSecondValueTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FullHouseBidWithNotValidValuesTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.NotValidValue, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FullHouseBidWithTwoTheSameValuesTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.Ace)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FlushBidWithSuccessTest()
    {
        var results = await Arrange
            .BidFlush(WhichPlayer.Conway, Suit.Clubs)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FlushBidWithNotValidSuitTest()
    {
        var results = await Arrange
            .BidFlush(WhichPlayer.Conway, Suit.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FourOfAKindBidWithSuccessTest()
    {
        var results = await Arrange
            .BidFourOfAKind(WhichPlayer.Conway, FaceCard.Nine)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task FourOfAKindBidWithNotValidValueTest()
    {
        var results = await Arrange
            .BidFourOfAKind(WhichPlayer.Conway, FaceCard.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task StraightFlushBidWithSuccessTest()
    {
        var results = await Arrange
            .BidStraightFlush(WhichPlayer.Conway, Suit.Diamonds)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task StraightFlushBidWithNotValidSuitTest()
    {
        var results = await Arrange
            .BidStraightFlush(WhichPlayer.Conway, Suit.NotValidValue)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task RoyalFlushBidWithSuccessTest()
    {
        var results = await Arrange
            .BidRoyalFlush(WhichPlayer.Conway, Suit.Spades)
            .Build();

        await Verify(results);
    }

    [Fact]
    public async Task RoyalFlushBidWithNotValidSuitTest()
    {
        var results = await Arrange
            .BidRoyalFlush(WhichPlayer.Conway, Suit.NotValidValue)
            .Build();

        await Verify(results);
    }
}