namespace Blef.Modules.Games.Domain.Entities;

public record PokerHand(FaceCard FaceCard)
{
    public static PokerHand Parse(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];
        if (string.Equals(pokerHandType, "one-of-a-kind", StringComparison.OrdinalIgnoreCase) == false)
        {
            throw new Exception($"Unknown value of poker hand type: '{pokerHandType}'");
        }

        return new PokerHand(ParseFaceCard(parts[1]));
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