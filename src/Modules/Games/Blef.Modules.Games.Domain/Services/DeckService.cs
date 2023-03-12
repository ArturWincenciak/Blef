namespace Blef.Modules.Games.Domain.Entities;

internal class DeckService : IDeckService
{
    private readonly List<Card> _cards;
    private readonly RandomnessProvider _randomnessProvider;

    public DeckService(RandomnessProvider randomnessProvider, List<Card> cards)
    {
        _randomnessProvider = randomnessProvider;
        _cards = cards;
    }

    public Card[] DealCards(int count)
    {
        List<Card> cards = new();

        for (var i = 0; i < count; i++)
            cards.Add(DealCard());

        return cards.ToArray();
    }

    private Card DealCard()
    {
        if (_cards.Any() == false)
            throw new InvalidOperationException("Cannot deal more cards from deck. Deck is empty");

        var randomPosition = _randomnessProvider.GetInt(_cards.Count);
        var card = _cards[randomPosition];
        _cards.RemoveAt(randomPosition);
        return card;
    }
}