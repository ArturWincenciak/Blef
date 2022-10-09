namespace Blef.Modules.Games.Domain.Entities;

public class Bids
{
    public static int Compare(string firstBid, string secondBid)
    {
        // TODO: implement more Poker Hands
        return Parse(secondBid).FaceCard.CompareTo(Parse(firstBid).FaceCard);
    }

    private static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];
        if (string.Equals(pokerHandType, "one-of-a-kind", StringComparison.OrdinalIgnoreCase) == false)
        {
            throw new Exception($"Unknown value of poker hand type: '{pokerHandType}'");
        }

        return new PokerHand(ParseRank(parts[1]));
    }

    private static FaceCard ParseRank(string faceCard)
    {
        return faceCard.ToLower() switch
        {
            "jack" => FaceCard.Jack,
            "queen" => FaceCard.Queen,
            "king" => FaceCard.King,
            "ace" => FaceCard.Ace,
            _ => throw new Exception($"Unknown value of FaceCard: '{faceCard}'")
        };
    }

    private record PokerHand(FaceCard FaceCard);
}