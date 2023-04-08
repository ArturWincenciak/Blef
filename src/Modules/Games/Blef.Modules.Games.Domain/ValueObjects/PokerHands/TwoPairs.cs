using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal sealed class TwoPairs : PokerHand
{
    public const string Type = "two-pairs";
    private readonly FaceCard _higher;
    private readonly FaceCard _lower;

    protected override int PokerHandRank => 3;

    public TwoPairs(FaceCard first, FaceCard second)
    {
        if (first is FaceCard.None) // todo: exception
            throw new ArgumentException("TOB");

        if (second is FaceCard.None) // todo: exception
            throw new ArgumentException("TBD");

        if (first == second) // todo: exception
            throw new ArgumentException("TBD");

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
        (10 * (int) _higher) + (int) _lower;

    public override string Serialize() =>
        $"{Type}:{_higher.ToString().ToLower()},{_lower.ToString().ToLower()}";
}