using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class FourPlayersPlayTheGame
{
    [Fact]
    public async Task Scenario()
    {
        var results = await new TestBuilder()

            .NewGame()
            .GetGameFlow()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Riemann)
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .Build();

        await Verify(results);
    }
}