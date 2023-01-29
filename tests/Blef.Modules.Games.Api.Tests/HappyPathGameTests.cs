namespace Blef.Modules.Games.Api.Tests;

public class HappyPathGameTests
{
    [Fact]
    public async Task PlayGame()
    {
        var exception = await Record.ExceptionAsync(async () =>
        {
            await new TestBuilder()
                .NewGame()
                .JoinPlayer(WhichPlayer.Knuth)
                .GetCards(WhichPlayer.Knuth)
                .JoinPlayer(WhichPlayer.Graham)
                .GetCards(WhichPlayer.Knuth)
                .GetCards(WhichPlayer.Graham)
                .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
                .Bid(WhichPlayer.Graham, "one-of-a-kind:ten")
                .Check(WhichPlayer.Knuth)
                .GetGameFlow()
                .Build();
        });

        Assert.Null(exception);
    }
}