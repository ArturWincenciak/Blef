using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal static class PokerHandParser
{
    public static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];

        //todo: implement more Poker Hands
        return pokerHandType.ToLower() switch
        {
            "high-card" => new HighCard(ParseFaceCard(parts[1])),
            "pair" => new Pair(ParseFaceCard(parts[1])),
            "two-pairs" => CreateTwoPairs(parts[1]),
            "low-straight" => new LowStraight(),
            "high-straight" => new HighStraight(),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
            //todo: validate, domain exception, test
        };
    }

    private static FaceCard ParseFaceCard(string faceCard) =>
        faceCard.ToLower() switch
        {
            "nine" => FaceCard.Nine,
            "ten" => FaceCard.Ten,
            "jack" => FaceCard.Jack,
            "queen" => FaceCard.Queen,
            "king" => FaceCard.King,
            "ace" => FaceCard.Ace,
            _ => throw new Exception($"Unknown value of FaceCard: '{faceCard}'")
        };

    private static TwoPairs CreateTwoPairs(string faceCards)
    {
        var faceCardParts = faceCards.Split(",");
        return new TwoPairs(first: ParseFaceCard(faceCardParts[0]), second: ParseFaceCard(faceCardParts[1]));
    }
}