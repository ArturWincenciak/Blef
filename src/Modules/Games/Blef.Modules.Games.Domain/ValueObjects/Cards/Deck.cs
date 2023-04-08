namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal sealed class Deck
{
    public const int NUMBER_OF_CARDS = 24;

    private readonly List<Card> _cards;

    public Deck(IEnumerable<Card> cards)
    {
        if (cards is null)
            throw new ArgumentNullException(nameof(cards));

        if (cards.Count() != NUMBER_OF_CARDS) // todo: exception
            throw new Exception("TBD");

        var isUnique = cards.Distinct().Count() == cards.Count();
        if (!isUnique) // todo: exception
            throw new Exception("TBD");

        _cards = cards.ToList();
    }

    public Hand Deal(CardsAmount cardsAmount)
    {
        // todo: validate if there are any cards left to be dealt
        var hand = _cards.Take(cardsAmount.Amount).ToArray();
        _cards.RemoveRange(index: 0, cardsAmount.Amount);
        return new(hand);
    }
}