﻿using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using static Blef.Modules.Games.Domain.Tests.Extensions.PokerHandFactory;

namespace Blef.Modules.Games.Domain.Tests.Extensions;

internal static class BidFactory
{
    public static void WithHighCardBid(Deal deal, DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenHighCard(faceCard);
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void WithHighCardBid(Game game, PlayerId byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenHighCard(faceCard);
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }

    public static void WithPairBid(Deal deal, DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenPair(faceCard);
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void WithTwoPairsBid(Deal deal, DealPlayer byPlayer, FaceCard firstFaceCard, FaceCard secondFaceCard)
    {
        var pokerHand = GivenTwoPairs(firstFaceCard, secondFaceCard);
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void WithLowStraight(Deal deal, DealPlayer byPlayer)
    {
        var pokerHand = GivenLowStraight();
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void WithLowStraight(Game game, PlayerId byPlayer)
    {
        var pokerHand = GivenLowStraight();
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }

    public static void WithHighStraight(Deal deal, DealPlayer byPlayer)
    {
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void WithHighStraight(Game game, PlayerId byPlayer)
    {
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }
}