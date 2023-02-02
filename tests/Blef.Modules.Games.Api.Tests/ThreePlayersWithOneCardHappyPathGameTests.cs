using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests;

public class ThreePlayersWithOneCardHappyPathGameTests
{
    [Fact]
    public async Task OneBidEachAndCheck() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .Bid(WhichPlayer.Knuth, bid: Bid.OneOfAKind.Nine)
            .Bid(WhichPlayer.Graham, bid: Bid.OneOfAKind.Ten)
            .Bid(WhichPlayer.Graham, bid: Bid.OneOfAKind.Jack)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .Build();
}