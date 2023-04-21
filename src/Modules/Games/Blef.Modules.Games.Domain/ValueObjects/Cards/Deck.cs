namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal sealed class Deck
{
    public const int NUMBER_OF_CARDS = 24;

    private readonly List<Card> _cards;

    public Deck(IEnumerable<Card> cards)
    {
        if (cards is null)
            throw new ArgumentNullException(nameof(cards));

        if (cards.Count() != NUMBER_OF_CARDS)
            throw new ArgumentOutOfRangeException($"The deck of cards must have exactly {NUMBER_OF_CARDS} cards");

        if (AreAllCardsUnique(cards) == false)
            throw new ArgumentOutOfRangeException("No card duplicates are allowed in the deck of cards");

        _cards = cards.ToList();
    }

    public Hand Deal(CardsAmount cardsAmount)
    {
        if (_cards.Count < cardsAmount)
            throw new InvalidOperationException("There are not enough cards in the deck to deal");

        var hand = _cards.Take(cardsAmount).ToArray();
        _cards.RemoveRange(index: 0, cardsAmount);
        return new(hand);
    }

    private static bool AreAllCardsUnique(IEnumerable<Card> cards) =>
        cards.Distinct().Count() == cards.Count();
}