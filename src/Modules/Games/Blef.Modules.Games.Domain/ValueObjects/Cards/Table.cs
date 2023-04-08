namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal sealed class Table
{
    private readonly IEnumerable<Hand> _hands;

    public IEnumerable<Card> Cards =>
        _hands.SelectMany(hand => hand.Cards);

    public Table(IEnumerable<Hand> hands)
    {
        // todo: check if all cards are unique in all hands
        // todo: check if there at least two hands (min players)
        // todo: check if there are not more then four hands (max players)

        _hands = hands ?? throw new ArgumentNullException(nameof(hands));
    }

    public bool HasFaceCard(FaceCard faceCard) =>
        Cards.Any(card => card.FaceCard == faceCard);
}