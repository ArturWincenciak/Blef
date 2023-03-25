using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

public class ThreePlayersWithOneCardTests
{
    [Fact]
    public async Task OneBidByEachAndCheck() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .Bid(WhichPlayer.Knuth, deal: 1, PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Graham, deal: 1, PokerHand.HighCard.Ten)
            .Bid(WhichPlayer.Graham, deal: 1, PokerHand.HighCard.Jack)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .Build();
}