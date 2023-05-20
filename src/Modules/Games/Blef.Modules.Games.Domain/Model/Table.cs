namespace Blef.Modules.Games.Domain.Model;

internal sealed class Table
{
    private readonly IReadOnlyCollection<Hand> _hands;

    public Table(IReadOnlyCollection<Hand> hands)
    {
        if (hands is null)
            throw new ArgumentNullException(nameof(hands));

        switch (hands.Count)
        {
            case < MIN_NUMBER_OF_PLAYERS:
                throw new ArgumentOutOfRangeException(paramName: nameof(hands), hands.Count,
                    message: $"The table should have at least {MIN_NUMBER_OF_PLAYERS} players' hands dealt");
            case > MAX_NUMBER_OF_PLAYERS:
                throw new ArgumentOutOfRangeException(paramName: nameof(hands), hands.Count,
                    message: $"There cannot be more than {MAX_NUMBER_OF_PLAYERS} players' hands dealt on the table");
        }

        if (AreAllCardsUnique(hands) == false)
            throw new ArgumentException("No card duplicates are allowed in the players' hands dealt on the table");

        _hands = hands;
    }

    public bool Contains(FaceCard faceCard) =>
        Cards.Any(card => card.FaceCard.Equals(faceCard));

    public int Count(FaceCard faceCard) =>
        Cards.Count(card => card.FaceCard.Equals(faceCard));

    private static bool AreAllCardsUnique(IEnumerable<Hand> hands)
    {
        var cardsInAllHands = hands.SelectMany(hand => hand.Cards).ToArray();
        var numberOfCardsInAllHands = cardsInAllHands.Length;
        return cardsInAllHands.Distinct().Count() == numberOfCardsInAllHands;
    }

    private IEnumerable<Card> Cards => _hands
        .SelectMany(hand => hand.Cards);
}