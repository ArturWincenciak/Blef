using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Core.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class PlayGameTests
{
    [Fact]
    public async Task OneBidByEachAndCheck()
    {
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .GetGameFlow()
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .NewDeal(WhichPlayer.Knuth)
            .GetCards(WhichPlayer.Knuth, deal: new(1))
            .GetCards(WhichPlayer.Graham, deal: new (1))
            .GetCards(WhichPlayer.Conway, deal: new (1))
            .Bid(WhichPlayer.Knuth, deal: new (1), PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Graham, deal: new (1), PokerHand.HighCard.Ten)
            .Bid(WhichPlayer.Conway, deal: new (1), PokerHand.HighCard.Jack)
            .Check(WhichPlayer.Knuth, deal: new (1))
            .GetDealFlow(deal: new (1))
            .Build();
    }
}