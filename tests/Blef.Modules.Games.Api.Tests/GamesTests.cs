using Microsoft.Extensions.Primitives;

namespace Blef.Modules.Games.Api.Tests;

public class GamesTests
{
    private const string GAMES = "games-module/games";

    [Fact]
    public async Task PlayGame()
    {
        var exception = await Record.ExceptionAsync(async () =>
        {
            await new TestBuilder()
                .WithNewGame()
                .WithJoinPlayer(TestBuilder.WhichPlayer.Knuth)
                .WithJoinPlayer(TestBuilder.WhichPlayer.Graham)
                .WithGetPlayerCard(TestBuilder.WhichPlayer.Knuth)
                .WithGetPlayerCard(TestBuilder.WhichPlayer.Graham)
                .WithBid(TestBuilder.WhichPlayer.Knuth, "one-of-a-kind:nine")
                .WithBid(TestBuilder.WhichPlayer.Graham, "one-of-a-kind:ten")
                .WithCheck(TestBuilder.WhichPlayer.Knuth)
                .WithGetGameFlow()
                .Build();
        });

        Assert.Null(exception);
    }
}