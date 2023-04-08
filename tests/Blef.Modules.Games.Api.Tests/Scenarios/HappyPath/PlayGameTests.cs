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
            .NewDeal(WhichPlayer.Knuth)
            .GetDealFlow(new DealNumber(1))
            .GetGameFlow()
            .GetCards(WhichPlayer.Knuth, deal: new DealNumber(1))
            .GetCards(WhichPlayer.Graham, deal: new DealNumber(1))
            .GetCards(WhichPlayer.Conway, deal: new DealNumber(1))
            .Bid(WhichPlayer.Knuth, deal: new DealNumber(1), PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Graham, deal: new DealNumber(1), PokerHand.HighCard.Ten)
            .Bid(WhichPlayer.Conway, deal: new DealNumber(1), PokerHand.HighCard.Jack)
            .Check(WhichPlayer.Knuth, deal: new DealNumber(1))
            .GetDealFlow(deal: new DealNumber(1))
            .GetGameFlow()
            .NewDeal(WhichPlayer.Graham)
            .GetDealFlow(new DealNumber(2))
            .GetGameFlow()
            .Build();

        return Verify(results);
    }
}