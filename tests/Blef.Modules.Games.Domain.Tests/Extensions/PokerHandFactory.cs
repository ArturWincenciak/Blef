using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.Extensions;

internal static class PokerHandFactory
{
    public static PokerHand GivenHighCard(FaceCard faceCard) =>
        HighCard.Create(faceCard.ToString());

    public static PokerHand GivenPair(FaceCard faceCard) =>
        Pair.Create(faceCard.ToString());

    public static PokerHand GivenTwoPairsBid(FaceCard firstFaceCard, FaceCard secondFaceCard) =>
        TwoPairs.Create($"{firstFaceCard},{secondFaceCard}");

    public static PokerHand GivenLowStraight() =>
        LowStraight.Create();

    public static PokerHand GivenHighStraight() =>
        HighStraight.Create();

    public static PokerHand GivenThreeOfAKind(FaceCard faceCard) =>
        ThreeOfAKind.Create(faceCard.ToString());

    public static PokerHand GivenFullHouse(FaceCard threeOfAKind, FaceCard pair) =>
        FullHouse.Create($"{threeOfAKind},{pair}");

    public static PokerHand GivenFlush(Suit suit) =>
        Flush.Create(suit.ToString());

    public static PokerHand GivenFourOfAKind(FaceCard faceCard) =>
        FourOfAKind.Create(faceCard.ToString());

    public static PokerHand GivenStraightFlush(Suit suit) =>
        StraightFlush.Create(suit.ToString());

    public static PokerHand GivenRoyalFlush(Suit suit) =>
        RoyalFlush.Create(suit.ToString());
}