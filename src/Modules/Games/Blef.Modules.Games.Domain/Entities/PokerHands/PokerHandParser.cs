namespace Blef.Modules.Games.Domain.Entities.PokerHands;

public static class PokerHandParser
{
    public static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];
        
        // TODO: implement more Poker Hands
        return pokerHandType.ToLower() switch
        {
            "one-of-a-kind" => new HighCard(ParseFaceCard(parts[1])),
            "high-card" => new HighCard(ParseFaceCard(parts[1])),
            "pair" => new Pair(ParseFaceCard(parts[1])),
            "two-pairs" => CreateTwoPairs(parts[1]),
            "low-straight" => new LowStraight(),
            "high-straight" => new HighStraight(),
            _ => throw new Exception($"Unknown type of poker hand: '{pokerHandType}'")
        };
    }

    private static TwoPairs CreateTwoPairs(string faceCards)
    {
        var faceCardParts = faceCards.Split(",");
        return new TwoPairs(ParseFaceCard(faceCardParts[0]), ParseFaceCard(faceCardParts[1]));
    }

    private static FaceCard ParseFaceCard(string faceCard)
    {
        return faceCard.ToLower() switch
        {
            "nine" => FaceCard.Nine,
            "ten" => FaceCard.Ten,
            "jack" => FaceCard.Jack,
            "queen" => FaceCard.Queen,
            "king" => FaceCard.King,
            "ace" => FaceCard.Ace,
            _ => throw new Exception($"Unknown value of FaceCard: '{faceCard}'")
        };
    }
}