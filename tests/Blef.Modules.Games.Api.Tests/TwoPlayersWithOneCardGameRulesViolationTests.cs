using System.Net;
using Blef.Modules.Games.Api.Tests.Core;

namespace Blef.Modules.Games.Api.Tests;

public class TwoPlayersWithOneCardGameRulesViolationTests
{
    [Fact]
    public async Task CannotPlayWithYourself() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .Bid(WhichPlayer.Knuth, PokerHand.HighCard.Nine,
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
            .Bid(WhichPlayer.Knuth, PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Knuth, PokerHand.HighCard.Ten,
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
            .Bid(WhichPlayer.Knuth, PokerHand.HighCard.Nine)
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

    [Fact]
    public async Task CannotCheckOnceAgainWhenGameIsAlreadyOver() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, PokerHand.HighCard.Nine)
            .Check(WhichPlayer.Graham)
            .Check(WhichPlayer.Graham,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        actual: problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "game-is-already-over",
                        actualString: problemDetails.Type);
                })
            .Build();

    [Fact]
    public async Task CannotBidWhenGameIsAlreadyOver() =>
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Bid(WhichPlayer.Knuth, PokerHand.HighCard.Nine)
            .Check(WhichPlayer.Graham)
            .Bid(WhichPlayer.Graham, PokerHand.HighCard.Nine,
                with: problemDetails =>
                {
                    Assert.Equal(
                        expected: (int) HttpStatusCode.BadRequest,
                        actual: problemDetails.Status);
                    Assert.Contains(
                        expectedSubstring: "game-is-already-over",
                        actualString: problemDetails.Type);
                })
            .Build();
}