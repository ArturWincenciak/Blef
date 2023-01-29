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
            .Bid(WhichPlayer.Knuth, bid: "one-of-a-kind:nine")
            .Bid(WhichPlayer.Graham, bid: "one-of-a-kind:ten")
            .Bid(WhichPlayer.Graham, bid: "one-of-a-kind:jack")
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .Build();
}