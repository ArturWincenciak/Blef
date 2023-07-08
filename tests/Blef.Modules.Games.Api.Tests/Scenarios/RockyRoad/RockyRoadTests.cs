using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.RockyRoad;

[UsesVerify]
public class RockyRoadTests
{
    private static TestBuilder Arrange => new TestBuilder()
        .NewGame()
        .JoinPlayer(WhichPlayer.Conway)
        .JoinPlayer(WhichPlayer.Graham)
        .NewDeal();

    [Fact]
    public async Task BidIsNotHigherThenLastOneTest()
    {
        var results = await Arrange
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King)
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task GameAlreadyStartedTest()
    {
        var results = await Arrange
            .NewDeal()
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task GameNotStartedTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task JoinGameAlreadyStartedTest()
    {
        var results = await Arrange
            .JoinPlayer(WhichPlayer.Knuth)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task NoBidToCheckTest()
    {
        var results = await Arrange
            .Check(WhichPlayer.Conway)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task NoBidToCheckOnLastDealFinishedTest()
    {
        var results = await Arrange
            .BidFlush(WhichPlayer.Conway, Suit.Hearts)
            .Check(WhichPlayer.Graham)
            .Check(WhichPlayer.Graham)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task NoPlayersNotEnoughPlayersTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .NewDeal()
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task OnePlayerIsNotEnoughPlayersTest()
    {
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .NewDeal()
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task PlayerAlreadyJoinedTest()
    {
        var results = new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Conway)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task PlayerAlreadyJoinedWithOtherPlayerTest()
    {
        var results = new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Conway)
            .Build();

        await Verify(results)
            .ScrubInlineGuids();
    }

    [Fact]
    public async Task WhenBidThatIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Build();

        await Verify(results)
            .ScrubInlineGuids()
            .ScrubEmptyLines();
    }

    [Fact]
    public async Task WhenCheckThatIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .Check(WhichPlayer.Graham)
            .Build();

        await Verify(results)
            .ScrubInlineGuids()
            .ScrubEmptyLines();
    }

    [Fact]
    public async Task WhenTwoBidsThatIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        await Verify(results)
            .ScrubInlineGuids()
            .ScrubEmptyLines();
    }

    [Fact]
    public async Task WhenBidAndCheckThatPlayerIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Conway)
            .Build();

        await Verify(results)
            .ScrubInlineGuids()
            .ScrubEmptyLines();
    }

    [Fact]
    public async Task WhenBidCheckAndBidPlayerIsNotThisPlayerTurnNowTest()
    {
        var results = await Arrange
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Check(WhichPlayer.Graham)
            .BidPair(WhichPlayer.Conway, FaceCard.Ace)
            .Build();

        await Verify(results)
            .ScrubInlineGuids()
            .ScrubEmptyLines();
    }

    // todo: turn not joined player
}