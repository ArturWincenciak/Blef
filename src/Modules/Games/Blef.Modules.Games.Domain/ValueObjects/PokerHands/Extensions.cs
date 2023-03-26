using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal static class Extensions
{
    public static bool HasFaceCard(this IEnumerable<Card> cards, FaceCard faceCard) =>
        cards.Any(card => card.FaceCard == faceCard);
}