using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests;

public class GameRulesViolationTests
{
    [Fact]
    public async Task CannotPlayWithYourself()
    {
        var exception = await Record.ExceptionAsync(async () =>
        {
            await new TestBuilder()
                .NewGame()
                .JoinPlayer(WhichPlayer.Knuth)
                .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
                .Build();
        });

        Assert.NotNull(exception);
    }

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