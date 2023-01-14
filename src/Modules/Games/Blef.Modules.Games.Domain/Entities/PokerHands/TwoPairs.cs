namespace Blef.Modules.Games.Domain.Entities.PokerHands;

public class TwoPairs : PokerHand
{
    private readonly FaceCard _first;
    private readonly FaceCard _second;

    protected override int PokerHandRank => 3;

    public TwoPairs(FaceCard first, FaceCard second)
    {
        if (first <= second)
            throw new ArgumentException($"First pair '{first}' has to be greater than second pair '{second}'");

        if (first == second)
            throw new ArgumentException($"First pair '{first}' has to be different than second pair '{second}'");

        _first = first;
        _second = second;
    }

    public override bool IsOnTable(IReadOnlyCollection<Card> table)
    {
        var firstFaceCardCount = table.Count(x => x.FaceCard == _first);
        var secondFaceCardCount = table.Count(x => x.FaceCard == _second);
        return firstFaceCardCount >= 2 && secondFaceCardCount >= 2;
    }

    protected override int GetInnerRank() =>
        (10 * (int) _first) + (int) _second;
}