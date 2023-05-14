namespace Blef.Modules.Games.Domain.Model;

internal sealed class Hand
{
    private const int MAX_CARD_AMOUNT = 5;

    public IEnumerable<Card> Cards { get; }

    public Hand(IEnumerable<Card> cards)
    {
        if (cards is null)
            throw new ArgumentNullException(nameof(cards));

        if (cards.Any() == false)
            throw new ArgumentOutOfRangeException(paramName: nameof(cards), actualValue: cards.Count(),
                message: "Hand without at least one card is not valid");

        if (cards.Count() > MAX_CARD_AMOUNT)
            throw new ArgumentOutOfRangeException(paramName: nameof(cards), actualValue: cards.Count(),
                message: "Hand with more then five cards is not valid");

        if (AreAllCardsUnique(cards) == false)
            throw new ArgumentException("No card duplicates are allowed in the players' hands");

        Cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }

    private static bool AreAllCardsUnique(IEnumerable<Card> cards) =>
        cards.Distinct().Count() == cards.Count();
}