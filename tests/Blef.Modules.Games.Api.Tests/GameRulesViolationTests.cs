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
}