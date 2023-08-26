using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.RockyRoad;

[UsesVerify]
public class GetCardsTests
{
    [Fact]
    public async Task GetCardsOfTheUserThatNotJoinedToTheGameTest()
    {
        // arrange
        var notJoinedPlayer = new PlayerId(Guid.Parse("C7E50E97-D576-4EF6-A657-AB1E34A806D7"));

        // act
        var results = await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Graham)
            .NewDeal()
            .GetCards(notJoinedPlayer, new DealNumber(1))
            .Build();

        // assert
        await Verify(results);

        // todo: check if the scenario is not covered by other tests in the RockyRoad folder
    }

    [Fact]
    public Task GetCardsOfTheUserThatLostTheGameTest()
    {
        // todo: implement this test
        // that scenario is not simple
        // that scenario is already covered by other tests
        // please see ThreePlayersPlayTheGameTest test
        // consider do not implement once again the same scenario
        // throw new NotImplementedException();
        return Task.CompletedTask;
    }
}