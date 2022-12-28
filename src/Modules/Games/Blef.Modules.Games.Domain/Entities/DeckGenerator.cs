namespace Blef.Modules.Games.Domain.Entities;

public class DeckGenerator
{
    private readonly RandomnessProvider _randomnessProvider;
    private readonly Lazy<IReadOnlyCollection<Card>> _cards = new(GetStartingDeck);

    public DeckGenerator(RandomnessProvider randomnessProvider)
    {
        _randomnessProvider = randomnessProvider;
    }

    public Deck GetFullDeck()
    {
        var readOnlyCollection = _cards.Value;

        var fullDeck = new Deck(_randomnessProvider, readOnlyCollection.ToList());

        return fullDeck;
    }

    /// <summary>
    /// This list is readonly so it can be reused by all Decks (<see cref="Deck"/>).
    /// <see cref="Card"/> is immutable and it is enough to have only one instance
    /// of each Card in the whole system.
    /// </summary>
    private static IReadOnlyCollection<Card> GetStartingDeck()
    {
        var cards = new List<Card>(24);

        foreach (var faceCard in CardsGenerator.GetAllFaceCards())
        {
            foreach (var suit in CardsGenerator.GetAllSuites())
            {
                cards.Add(new Card(faceCard, suit));
            }
        }

        return cards.AsReadOnly();
    }
}