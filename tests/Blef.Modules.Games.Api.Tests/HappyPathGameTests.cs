using System.Runtime.InteropServices;
using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests;

public class HappyPathGameTests
{
    [Fact]
    public async Task PlayGame()
    {
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .GetCards(WhichPlayer.Knuth, with: AssertCards())
            .JoinPlayer(WhichPlayer.Graham)
            .GetCards(WhichPlayer.Knuth, with: AssertCards())
            .GetCards(WhichPlayer.Graham, with: AssertCards())
            .Bid(WhichPlayer.Knuth, "one-of-a-kind:nine")
            .Bid(WhichPlayer.Graham, "one-of-a-kind:ten")
            .Check(WhichPlayer.Knuth)
            .GetGameFlow(with: (result, game) =>
            {
                Assert.Equal(expected: 2, result.Players.Length);
                Assert.Equal(expected: 2, result.Bids.Length);
                Assert.True(result.CheckingPlayerId != Guid.Empty);
                Assert.True(result.LooserPlayerId != Guid.Empty);

                var knuth = result.Players[0];
                Assert.Equal(expected: game.KnuthPlayerId, knuth.Id);
                Assert.Equal(expected: WhichPlayer.Knuth.ToString(), knuth.Nick);
                Assert.Single(knuth.Cards);

                var graham = result.Players[1];
                Assert.Equal(expected: game.GrahamPlayerId, graham.Id);
                Assert.Equal(expected: WhichPlayer.Graham.ToString(), graham.Nick);
                Assert.Single(graham.Cards);

                var firstBid = result.Bids.Single(bid => bid.Order == 1);
                Assert.Equal(expected: knuth.Id, firstBid.PlayerId);
                Assert.Equal(expected: "one-of-a-kind:nine", firstBid.Bid);

                var secondBid = result.Bids.Single(bid => bid.Order == 2);
                Assert.Equal(expected: graham.Id, secondBid.PlayerId);
                Assert.Equal(expected: "one-of-a-kind:ten", secondBid.Bid);

                Assert.Equal(game.KnuthPlayerId, result.CheckingPlayerId);
                Assert.True(result.LooserPlayerId == knuth.Id || result.LooserPlayerId == graham.Id);
            })
            .Build();

        Action<GetPlayerCards.Result> AssertCards() => result =>
        {
            Assert.Single(result.Cards);
            Assert.NotEmpty(result.Cards[0].FaceCard);
            Assert.NotEmpty(result.Cards[0].Suit);
        };
    }
}