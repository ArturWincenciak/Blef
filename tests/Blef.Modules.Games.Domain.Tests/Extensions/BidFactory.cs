using Blef.Modules.Games.Domain.Model;
using static Blef.Modules.Games.Domain.Tests.Extensions.PokerHandFactory;

namespace Blef.Modules.Games.Domain.Tests.Extensions;

internal static class BidFactory
{
    public static void PlayHighCardBid(Deal deal, DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenHighCard(faceCard);
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void PlayHighCardBid(Game game, PlayerId byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenHighCard(faceCard);
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }

    public static void PlayPairBid(Deal deal, DealPlayer byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenPair(faceCard);
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void PlayPairBid(Game game, PlayerId byPlayer, FaceCard faceCard)
    {
        var pokerHand = GivenPair(faceCard);
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }

    public static void PlayTwoPairsBid(Deal deal, DealPlayer byPlayer, FaceCard firstFaceCard, FaceCard secondFaceCard)
    {
        var pokerHand = GivenTwoPairsBid(firstFaceCard, secondFaceCard);
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void PlayLowStraightBid(Deal deal, DealPlayer byPlayer)
    {
        var pokerHand = GivenLowStraight();
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void PlayLowStraightBid(Game game, PlayerId byPlayer)
    {
        var pokerHand = GivenLowStraight();
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }

    public static void PlayHighStraightBid(Deal deal, DealPlayer byPlayer)
    {
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, byPlayer.Player);
        deal.Bid(bid);
    }

    public static void PlayHighStraightBid(Game game, PlayerId byPlayer)
    {
        var pokerHand = GivenHighStraight();
        var bid = new Bid(pokerHand, byPlayer);
        game.Bid(bid);
    }
}