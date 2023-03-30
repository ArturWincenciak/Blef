namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal class Deck
{
    public const int NUMBER_OF_CARDS = 24;

    private readonly List<Card> _cards;

    public Deck(IEnumerable<Card> cards)
    {
        if (cards is null)
            throw new ArgumentNullException(nameof(cards));

        if (cards.Count() != NUMBER_OF_CARDS) // todo: exception
            throw new Exception("TBD");

        bool isUnique = cards.Distinct().Count() == cards.Count();
        if (!isUnique) // todo: exception
            throw new Exception("TBD");

        foreach (var card in cards)
            if (card.FaceCard == FaceCard.None || card.Suit == Suit.None)
                throw new Exception("TBD"); // todo: exception

        _cards = cards.ToList();
    }

    public IEnumerable<Card> Deal(CardsAmount cardsAmount)
    {
        // todo: get last card and remove the card form the deck
        // todo: validate if there are any cards left to be dealt
        var dealtCards = _cards.Take(cardsAmount.Value);
        _cards.RemoveRange(0, cardsAmount.Value);
        return dealtCards;
    }
}