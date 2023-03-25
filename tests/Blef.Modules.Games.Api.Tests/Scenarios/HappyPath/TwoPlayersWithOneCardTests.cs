﻿using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Core.ValueObjects;
using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain.Entities;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

public class TwoPlayersWithOneCardTests
{
    [Fact]
    public async Task OneBidByEachAndCheck()
    {
        await new TestBuilder()
            .NewGame()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .Deal(WhichPlayer.Knuth)
            .GetCards(WhichPlayer.Knuth, deal: 1, AssertThatPlayerShouldHaveOneCard)
            .GetCards(WhichPlayer.Graham, deal: 1, AssertThatPlayerShouldHaveOneCard)
            .Bid(WhichPlayer.Knuth, deal: 1, PokerHand.HighCard.Nine)
            .Bid(WhichPlayer.Graham, deal: 1, PokerHand.HighCard.Ten)
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
            Assert.Equal(game.KnuthPlayerId, knuth.Id);
            Assert.Equal(expected: WhichPlayer.Knuth.ToString(), knuth.Nick);
            Assert.Single(knuth.Cards);

            var graham = result.Players[1];
            Assert.Equal(game.GrahamPlayerId, graham.Id);
            Assert.Equal(expected: WhichPlayer.Graham.ToString(), graham.Nick);
            Assert.Single(graham.Cards);

            var firstBid = result.Bids.Single(bid => bid.Order == 1);
            Assert.Equal(knuth.Id, firstBid.PlayerId);
            Assert.Equal(PokerHand.HighCard.Nine, firstBid.Bid);

            var secondBid = result.Bids.Single(bid => bid.Order == 2);
            Assert.Equal(graham.Id, secondBid.PlayerId);
            Assert.Equal(PokerHand.HighCard.Ten, secondBid.Bid);

            Assert.Equal(game.KnuthPlayerId, result.CheckingPlayerId);
            Assert.True(result.LooserPlayerId == knuth.Id || result.LooserPlayerId == graham.Id);
        }
    }
}