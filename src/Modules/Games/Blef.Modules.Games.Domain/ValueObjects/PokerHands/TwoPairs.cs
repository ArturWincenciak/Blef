using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal class TwoPairs : PokerHand
{
    public const string Type = "two-pairs";
    private readonly FaceCard _first;
    private readonly FaceCard _second;

    protected override int PokerHandRank => 3;

    public TwoPairs(FaceCard first, FaceCard second)
    {
        if (first <= second)
            throw new ArgumentException($"First pair '{first}' has to be greater than second pair '{second}'");

        _first = first;
        _second = second;
    }

    public override bool IsOnTable(Table table)
    {
        var firstFaceCardCount = table.Cards.Count(x => x.FaceCard == _first);
        var secondFaceCardCount = table.Cards.Count(x => x.FaceCard == _second);
        return firstFaceCardCount >= 2 && secondFaceCardCount >= 2;
    }

    protected override int GetInnerRank() =>
        (10 * (int) _first) + (int) _second;

    public override string Serialize() =>
        $"{Type}:{_first.ToString().ToLower()},{_second.ToString().ToLower()}";
}