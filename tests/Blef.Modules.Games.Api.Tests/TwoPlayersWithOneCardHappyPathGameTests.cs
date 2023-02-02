using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Application.Queries;

namespace Blef.Modules.Games.Api.Tests;

public class TwoPlayersWithOneCardHappyPathGameTests
{
    [Fact]
    public async Task OneBidEachAndCheck()
    {
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .GetCards(WhichPlayer.Knuth, with: AssertThatPlayerShouldHaveOneCard)
            .JoinPlayer(WhichPlayer.Graham)
            .GetCards(WhichPlayer.Knuth, with: AssertThatPlayerShouldHaveOneCard)
            .GetCards(WhichPlayer.Graham, with: AssertThatPlayerShouldHaveOneCard)
            .Bid(WhichPlayer.Knuth, bid: PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Graham, bid: PokerHand.HighCard.Ten)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow(with: AssertGameState)
            .Build();

        void AssertThatPlayerShouldHaveOneCard(GetPlayerCards.Result result)
        {
            Assert.Single(result.Cards);
            Assert.NotEmpty(result.Cards[0].FaceCard);
            Assert.NotEmpty(result.Cards[0].Suit);
        }

        void AssertGameState(GetGameFlow.Result result, BlefClient.State game)
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
            Assert.Equal(expected: PokerHand.HighCard.Nine, firstBid.Bid);

            var secondBid = result.Bids.Single(bid => bid.Order == 2);
            Assert.Equal(expected: graham.Id, secondBid.PlayerId);
            Assert.Equal(expected: PokerHand.HighCard.Ten, secondBid.Bid);

            Assert.Equal(game.KnuthPlayerId, result.CheckingPlayerId);
            Assert.True(result.LooserPlayerId == knuth.Id || result.LooserPlayerId == graham.Id);
        }
    }
}