using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.TestsOfTests;

public class BlefClientTests
{
    [Fact]
    public void GivenWrongEnumPlayerInJoinMethodThenThrowArgumentOutOfRangeException() =>
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await new TestBuilder()
                .NewGame()
                .JoinPlayer((WhichPlayer) 128)
                .Build());

    [Fact]
    public void GivenWrongEnumPlayerInGetCardsMethodThenThrowArgumentOutOfRangeException() =>
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(async () =>
            await new TestBuilder()
                .NewGame()
                .JoinPlayer(WhichPlayer.Conway)
                .JoinPlayer(WhichPlayer.Graham)
                .NewDeal()
                .GetCards(whichPlayer: (WhichPlayer) 128, deal: new DealNumber(1))
                .Build());
}