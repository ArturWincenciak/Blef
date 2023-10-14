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

    public static PokerHand Map(string bid)
    {
        var parts = bid.Split(":");
        var pokerHandType = parts[0];

        return pokerHandType.ToLower() switch
        {
            HighCard.TYPE => HighCard.Create(PokerHandValue()),
            Pair.TYPE => Pair.Create(PokerHandValue()),
            TwoPairs.TYPE => TwoPairs.Create(PokerHandValue()),
            LowStraight.TYPE => LowStraight.Create(),
            HighStraight.TYPE => HighStraight.Create(),
            ThreeOfAKind.TYPE => ThreeOfAKind.Create(PokerHandValue()),
            FullHouse.TYPE => FullHouse.Create(PokerHandValue()),
            Flush.TYPE => Flush.Create(PokerHandValue()),
            FourOfAKind.TYPE => FourOfAKind.Create(PokerHandValue()),
            StraightFlush.TYPE => StraightFlush.Create(PokerHandValue()),
            RoyalFlush.TYPE => RoyalFlush.Create(PokerHandValue()),
            _ => throw new ArgumentOutOfRangeException(
                paramName: nameof(bid), message: $"Unknown type of poker hand: '{pokerHandType}'")
        };

        string PokerHandValue() => parts[1];
    }
}