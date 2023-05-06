namespace Blef.Modules.Games.Domain.Model.PokerHands;

/// <summary>
///     Ranks should be assigned according to
///     <see href="https://pl.wikipedia.org/wiki/Blef_(gra)" />
/// </summary>
internal abstract class PokerHand
{
    protected abstract int PokerHandRank { get; }

    public abstract bool IsOnTable(Table table);

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

    public abstract string Serialize();
}