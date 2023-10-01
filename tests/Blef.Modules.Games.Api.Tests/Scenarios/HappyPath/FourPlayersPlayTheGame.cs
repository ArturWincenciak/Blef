﻿using Blef.Modules.Games.Api.Tests.Core;
using Blef.Modules.Games.Api.Tests.Scenarios.ValueObjects;

namespace Blef.Modules.Games.Api.Tests.Scenarios.HappyPath;

[UsesVerify]
public class FourPlayersPlayTheGame
{
    [Fact] // todo: RED
    public async Task Scenario()
    {
        var results = await new TestBuilder()

            .NewGame()
            .GetGameFlow()
            .JoinPlayer(WhichPlayer.Knuth)
            .JoinPlayer(WhichPlayer.Graham)
            .JoinPlayer(WhichPlayer.Conway)
            .JoinPlayer(WhichPlayer.Riemann)
            .NewDeal()
            .GetGameFlow()
            .GetDealFlow(new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(1))
            .GetCards(WhichPlayer.Graham, new DealNumber(1))
            .GetCards(WhichPlayer.Conway, new DealNumber(1))
            .GetCards(WhichPlayer.Riemann, new DealNumber(1))
            .BidHighCard(WhichPlayer.Knuth, FaceCard.Nine)
            .BidHighCard(WhichPlayer.Graham, FaceCard.Ten)
            .BidHighCard(WhichPlayer.Conway, FaceCard.Jack)
            .BidHighCard(WhichPlayer.Riemann, FaceCard.Queen)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(1))

            .GetCards(WhichPlayer.Knuth, new DealNumber(2))
            .GetCards(WhichPlayer.Graham, new DealNumber(2))
            .GetCards(WhichPlayer.Conway, new DealNumber(2))
            .GetCards(WhichPlayer.Riemann, new DealNumber(2))
            .BidPair(WhichPlayer.Graham, FaceCard.Ace)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(2))

            .GetCards(WhichPlayer.Knuth, new DealNumber(3))
            .GetCards(WhichPlayer.Graham, new DealNumber(3))
            .GetCards(WhichPlayer.Conway, new DealNumber(3))
            .GetCards(WhichPlayer.Riemann, new DealNumber(3))
            .BidPair(WhichPlayer.Conway, FaceCard.King)
            .BidThreeOfAKind(WhichPlayer.Knuth, FaceCard.Ace)
            .BidThreeOfAKind(WhichPlayer.Riemann, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(3))

            .GetCards(WhichPlayer.Knuth, new DealNumber(4))
            .GetCards(WhichPlayer.Graham, new DealNumber(4))
            .GetCards(WhichPlayer.Conway, new DealNumber(4))
            .GetCards(WhichPlayer.Riemann, new DealNumber(4))
            .BidFourOfAKind(WhichPlayer.Knuth, FaceCard.Nine)
            .BidStraightFlush(WhichPlayer.Riemann, Suit.Clubs)
            .Check(WhichPlayer.Conway)
            .BidHighStraight(WhichPlayer.Knuth)
            .Check(WhichPlayer.Graham)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(4))

            .GetCards(WhichPlayer.Knuth, new DealNumber(5))
            .GetCards(WhichPlayer.Graham, new DealNumber(5))
            .GetCards(WhichPlayer.Conway, new DealNumber(5))
            .GetCards(WhichPlayer.Riemann, new DealNumber(5))
            .BidStraightFlush(WhichPlayer.Knuth, Suit.Clubs)
            .Check(WhichPlayer.Graham)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(5))

            .GetCards(WhichPlayer.Knuth, new DealNumber(6))
            .GetCards(WhichPlayer.Graham, new DealNumber(6))
            .GetCards(WhichPlayer.Conway, new DealNumber(6))
            .GetCards(WhichPlayer.Riemann, new DealNumber(6))
            .BidHighStraight(WhichPlayer.Graham)
            .Check(WhichPlayer.Conway)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(6))

            .GetCards(WhichPlayer.Knuth, new DealNumber(7))
            .GetCards(WhichPlayer.Graham, new DealNumber(7))
            .GetCards(WhichPlayer.Conway, new DealNumber(7))
            .GetCards(WhichPlayer.Riemann, new DealNumber(7))
            .BidFullHouse(WhichPlayer.Conway, FaceCard.Ace, FaceCard.King)
            .Check(WhichPlayer.Riemann)
            .GetGameFlow()
            .GetDealFlow(deal: new DealNumber(7))

            .GetCards(WhichPlayer.Knuth, new DealNumber(8))
            .GetCards(WhichPlayer.Graham, new DealNumber(8))
            .GetCards(WhichPlayer.Conway, new DealNumber(8))
            .GetCards(WhichPlayer.Riemann, new DealNumber(8))
            .BidTwoPairs(WhichPlayer.Riemann, FaceCard.Ace, FaceCard.King)
            .BidTwoPairs(WhichPlayer.Knuth, FaceCard.King, FaceCard.Ace)
            .BidThreeOfAKind(WhichPlayer.Knuth, FaceCard.King)
            .BidThreeOfAKind(WhichPlayer.Graham, FaceCard.Ace)
            .BidFourOfAKind(WhichPlayer.Conway, FaceCard.King)
            .BidFourOfAKind(WhichPlayer.Riemann, FaceCard.Ace)
            .Check(WhichPlayer.Knuth)
            .GetGameFlow()

            .GetCards(WhichPlayer.Knuth, new DealNumber(9))
            .GetCards(WhichPlayer.Graham, new DealNumber(9))
            .GetCards(WhichPlayer.Conway, new DealNumber(9))
            .GetCards(WhichPlayer.Riemann, new DealNumber(9))

            // todo: there is a bug, this deal should start Knuth (not Conway)
            // note: last deal has been started by Riemann
            // note: last deal has been lost by Riemann
            // note: this deal should start Knuth (not by Conway)
            .BidTwoPairs(WhichPlayer.Knuth, FaceCard.Ace, FaceCard.King)

            .Build();

        await Verify(results);
    }
}