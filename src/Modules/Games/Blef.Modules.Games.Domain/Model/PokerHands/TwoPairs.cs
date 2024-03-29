﻿namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class TwoPairs : PokerHand
{
    public const string TYPE = "two-pairs";
    private readonly FaceCard _higher;
    private readonly FaceCard _lower;

    protected override int PokerHandRank => 3;

    private TwoPairs(FaceCard first, FaceCard second)
    {
        if (first.Equals(second))
            throw new ArgumentException("Two pairs cannot have the same rank");

        (_higher, _lower) = first.IsBetterThen(second)
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
        (100 * _higher.GetRank()) + _lower.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_higher.ToString().ToLower()},{_lower.ToString().ToLower()}";

    public static PokerHand Create(string faceCards)
    {
        var faceCardParts = faceCards.Split(",");
        return new TwoPairs(
            first: FaceCard.Create(faceCardParts[0]),
            second: FaceCard.Create(faceCardParts[1]));
    }
}