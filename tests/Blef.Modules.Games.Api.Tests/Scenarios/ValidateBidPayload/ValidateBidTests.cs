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
}
