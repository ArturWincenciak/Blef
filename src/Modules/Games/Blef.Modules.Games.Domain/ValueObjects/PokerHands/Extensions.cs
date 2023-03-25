namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

public static class Extensions
{
    public static bool HasFaceCard(this IEnumerable<Card> cards, FaceCard faceCard) =>
        cards.Any(card => card.FaceCard == faceCard);
}