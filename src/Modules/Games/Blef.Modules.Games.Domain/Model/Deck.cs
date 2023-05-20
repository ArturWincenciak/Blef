namespace Blef.Modules.Games.Domain.Model;

internal sealed class Deck
{
    public const int NUMBER_OF_CARDS = 24;

    private readonly List<Card> _cards;

    public Deck(IReadOnlyCollection<Card> cards)
    {
        if (cards is null)
            throw new ArgumentNullException(nameof(cards));

        if (cards.Count != NUMBER_OF_CARDS)
            throw new ArgumentOutOfRangeException(paramName: nameof(cards), cards.Count,
                message: $"The deck of cards must have exactly {NUMBER_OF_CARDS} cards");

        if (AreAllCardsUnique(cards) == false)
            throw new ArgumentOutOfRangeException(paramName: nameof(cards), cards.Count,
                message: "No card duplicates are allowed in the deck of cards");

        _cards = cards.ToList();
    }

    public Hand Deal(CardsAmount cardsAmount)
    {
        if (_cards.Count < cardsAmount)
            throw new InvalidOperationException("There are not enough cards in the deck to deal");

        var hand = _cards.Take(cardsAmount).ToArray();
        _cards.RemoveRange(index: 0, cardsAmount);
        return new Hand(hand);
    }

    private static bool AreAllCardsUnique(IReadOnlyCollection<Card> cards) =>
        cards.Distinct().Count() == cards.Count;
}