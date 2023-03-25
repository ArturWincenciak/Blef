﻿using System.Net;
using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Core.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.BreakingRules;

public class TwoPlayersWithOneCardTests
{
    [Fact]
    public async Task CannotPlayWithYourself() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .Bid(WhichPlayer.Knuth, deal: new(1), PokerHand.HighCard.Nine,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "minimum-game-players-not-reached",
                        problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotBidTwiceInTheSameRound() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, deal: new(1), PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Knuth, deal: new(1), PokerHand.HighCard.Ten,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "that-is-not-this-player-turn-now",
                        problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotBidAndCheckInTheSameRound() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, deal: new(1), PokerHand.HighCard.Nine)
            .Check(WhichPlayer.Knuth, deal: new(1),
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "that-is-not-this-player-turn-now",
                        problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task FirstMoveInGameCannotBeCheck() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Check(WhichPlayer.Knuth, deal: new(1),
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "no-bid-to-check",
                        problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotCheckOnceAgainWhenGameIsAlreadyOver() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, deal: new(1), PokerHand.HighCard.Nine)
            .Check(WhichPlayer.Graham, deal: new(1))
            .Check(WhichPlayer.Graham, deal: new(1),
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "game-is-already-over",
                        problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotBidWhenGameIsAlreadyOver() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, deal: new(1), PokerHand.HighCard.Nine)
            .Check(WhichPlayer.Graham, deal: new(1))
            .Bid(WhichPlayer.Graham, deal: new(1), PokerHand.HighCard.Nine,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "game-is-already-over",
                        problemDetails.Type);
                })
            .Build();
}