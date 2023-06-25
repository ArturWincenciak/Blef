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
    public Task HighCardBidWithFailureTest()
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
    public Task TwoPairsBidWithSuccessTest()
    {
        var results = Arrange
            .BidTwoPairs(WhichPlayer.Conway, FaceCard.Jack, FaceCard.Queen)
            .Build();

        // todo: test case: bid two pairs with two the same face cards should fail

        return Verify(results);
    }
}
