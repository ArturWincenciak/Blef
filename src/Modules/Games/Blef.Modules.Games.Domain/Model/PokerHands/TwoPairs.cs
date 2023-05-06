namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class TwoPairs : PokerHand
{
    public const string Type = "two-pairs";
    private readonly FaceCard _higher;
    private readonly FaceCard _lower;

    protected override int PokerHandRank => 3;

    private TwoPairs(FaceCard first, FaceCard second)
    {
        if (first == second)
            throw new ArgumentException("Two pairs cannot have the same rank");

        (_higher, _lower) = first > second
            ? (first, second)
            : (second, first);
    }

    public override bool IsOnTable(Table table)
    {
        var higherFaceCardCount = table.Count(_higher);
        var lowerFaceCardCount = table.Count(_lower);
        return higherFaceCardCount >= 2 && lowerFaceCardCount >= 2;
    }

    protected override int GetInnerRank() =>
        (10 * _higher.GetRank()) + _lower.GetRank();

    public override string Serialize() =>
        $"{Type}:{_higher.ToString().ToLower()},{_lower.ToString().ToLower()}";

    public static PokerHand Create(string pokerHand)
    {
        var faceCardParts = pokerHand.Split(",");
        return new TwoPairs(first: FaceCard.Create(faceCardParts[0]), FaceCard.Create(faceCardParts[1]));
    }
}