using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

public class ThreePlayersWithOneCardTests
{
    [Fact(Skip = "todo")]
    public async Task OneBidByEachAndCheck() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .Bid(WhichPlayer.Knuth, bid: PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Graham, bid: PokerHand.HighCard.Ten)
            .Bid(WhichPlayer.Graham, bid: PokerHand.HighCard.Jack)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .Build();
}