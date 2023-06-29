using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class PlayGameTests
{
    [Fact]
    public Task ThreePlayersPlayTheGameTest()
    {
        var results = new TestBuilder()
            .NewGame()
            .GetGameFlow()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .GetGameFlow()
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(1))
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(1))
            .GetCards(WhichPlayer.Conway, deal: new DealNumber(1))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ten)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Jack)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(1))
            .GetDealFlow(new DealNumber(2))
            .Build();

        return Verify(results);
    }
}