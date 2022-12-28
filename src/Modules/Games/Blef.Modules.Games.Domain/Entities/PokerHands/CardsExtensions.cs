namespace Blef.Modules.Games.Domain.Entities.PokerHands;

public static class CardsExtensions
{
    public static bool HasFaceCard(this IEnumerable<Card> cards, FaceCard faceCard)
    {
        return cards.Any(card => card.FaceCard == faceCard);
    }
}