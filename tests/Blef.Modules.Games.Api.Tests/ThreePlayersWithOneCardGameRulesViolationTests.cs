﻿using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests;

public class ThreePlayersWithOneCardGameRulesViolationTests
{
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