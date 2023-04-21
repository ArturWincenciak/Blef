namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal sealed class Table
{
    private const int MAX_NUMBER_OF_PLAYERS = 4;
    private const int MIN_NUMBER_OF_PLAYERS = 2;

    private readonly IEnumerable<Hand> _hands;

    private IEnumerable<Card> Cards =>
        _hands.SelectMany(hand => hand.Cards);

    public Table(IEnumerable<Hand> hands)
    {
        if(hands is null)
            throw new ArgumentNullException(nameof(hands));

        if (hands.Count() < MIN_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException("The table should have at least two players' hands dealt");

        if (hands.Count() > MAX_NUMBER_OF_PLAYERS)
            throw new ArgumentOutOfRangeException("There cannot be more than four players' hands dealt on the table");

        if (AreAllCardsUnique(hands) == false)
            throw new ArgumentException("No card duplicates are allowed in the players' hands dealt on the table");

        _hands = hands;
    }

    public bool Contains(FaceCard faceCard) =>
        Cards.Any(card => card.FaceCard == faceCard);

    public int Count(FaceCard faceCard) =>
        Cards.Count(card => card.FaceCard == faceCard);

    private bool AreAllCardsUnique(IEnumerable<Hand> hands)
    {
        var cardsInAllHands = hands.SelectMany(hand => hand.Cards);
        var numberOfCardsInAllHands = cardsInAllHands.Count();
        return cardsInAllHands.Distinct().Count() == numberOfCardsInAllHands;
    }
}