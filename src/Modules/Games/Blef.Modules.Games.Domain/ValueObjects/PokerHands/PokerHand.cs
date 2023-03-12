namespace Blef.Modules.Games.Domain.Entities.PokerHands;

/// <summary>
///     Ranks should be assigned according to
///     <see href="https://pl.wikipedia.org/wiki/Blef_(gra)" />
/// </summary>
public abstract class PokerHand
{
    protected abstract int PokerHandRank { get; }

    public abstract bool IsOnTable(IReadOnlyCollection<Card> table);

    public bool IsBetterThan(PokerHand otherPokerHand) =>
        CompareWith(otherPokerHand) > 0;

    private int CompareWith(PokerHand otherPokerHand)
    {
        var genericValueResult = PokerHandRank - otherPokerHand.PokerHandRank;

        if (genericValueResult != 0)
            return genericValueResult;

        return GetInnerRank() - otherPokerHand.GetInnerRank();
    }

    protected abstract int GetInnerRank();
}