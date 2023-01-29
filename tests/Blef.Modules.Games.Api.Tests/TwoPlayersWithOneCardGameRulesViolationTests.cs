﻿using System.Net;
using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests;

public class TwoPlayersWithOneCardGameRulesViolationTests
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
    public async Task CannotBidTwiceInTheSameRound() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
            .Bid(WhichPlayer.Knuth, "one-of-a-kind:ten",
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        actual: problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "that-is-not-this-player-turn-now",
                        actualString: problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotBidAndCheckInTheSameRound() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
            .Check(WhichPlayer.Knuth,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        actual: problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "that-is-not-this-player-turn-now",
                        actualString: problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task FirstMoveInGameCannotBeCheck() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Check(WhichPlayer.Knuth,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        actual: problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "no-bid-to-check",
                        actualString: problemDetails.Type);
                })
            .Build();
}