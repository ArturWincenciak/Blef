using System.Security.Cryptography;
using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class ShuffledDeckFactory : IDeckFactory
{
    private readonly static IEnumerable<Suit> _suites = new[]
    {
        Suit.Clubs,
        Suit.Diamonds,
        Suit.Hearts,
        Suit.Spades
    };

    private readonly static IEnumerable<FaceCard> _faceCards = new[]
    {
        FaceCard.Nine,
        FaceCard.Ten,
        FaceCard.Jack,
        FaceCard.Queen,
        FaceCard.King,
        FaceCard.Ace
    };

    public Deck Create()
    {
        var orderedCards = OrderedCards();
        var shuffledCards = ShuffledCards(orderedCards);
        return new Deck(shuffledCards.ToArray());
    }

    private static List<Card> OrderedCards()
    {
        var cards = new List<Card>();

        foreach (var suite in _suites)
        {
            foreach (var faceCard in _faceCards)
                cards.Add(new Card(faceCard, suite));
        }

        return cards;
    }

    private static List<Card> ShuffledCards(List<Card> cards)
    {
        var shuffledCards = new List<Card>();

        for (var maxCardIndex = Deck.NUMBER_OF_CARDS - 1; maxCardIndex > 0; maxCardIndex--)
        {
            var randomIndex = RandomNumberGenerator.GetInt32(fromInclusive: 0, maxCardIndex);
            var randomCard = cards[randomIndex];
            cards.RemoveAt(randomIndex);
            shuffledCards.Add(randomCard);
        }

        shuffledCards.Add(cards[0]);

        return shuffledCards;
    }
}