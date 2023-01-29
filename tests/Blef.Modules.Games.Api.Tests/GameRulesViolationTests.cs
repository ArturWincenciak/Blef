using System.Net;
using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests;

public class GameRulesViolationTests
{
    [Fact]
    public async Task CannotPlayWithYourself() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine",
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        actual: problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "minimum-game-players-not-reached",
                        actualString: problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotJoinWhenGameHasBeenStarted()
    {
        var exception = await Record.ExceptionAsync(async () =>
        {
            await new TestBuilder()
                .NewGame()
                .JoinPlayer(WhichPlayer.Knuth)
                .JoinPlayer(WhichPlayer.Graham)
                .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
                .JoinPlayer(WhichPlayer.Conway)
                .Build();
        });

        Assert.NotNull(exception);
    }
}