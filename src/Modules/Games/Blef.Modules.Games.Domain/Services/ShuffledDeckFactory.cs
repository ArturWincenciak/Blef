using System.Security.Cryptography;
using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Services;

internal sealed class ShuffledDeckFactory : IDeckFactory
{
    private readonly static IReadOnlyCollection<Suit> Suites = new[]
    {
        Suit.Clubs,
        Suit.Diamonds,
        Suit.Hearts,
        Suit.Spades
    };

    private readonly static IReadOnlyCollection<FaceCard> FaceCards = new[]
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
        return new(shuffledCards);
    }

    private static List<Card> OrderedCards()
    {
        var cards = new List<Card>();

        foreach (var suite in Suites)
        {
            foreach (var faceCard in FaceCards)
                cards.Add(new(faceCard, suite));
        }

        return cards;
    }

    private static IReadOnlyCollection<Card> ShuffledCards(IList<Card> cards)
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