using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal static class Extensions
{
    // todo: avoid this extension
    public static bool HasFaceCard(this Table table, FaceCard faceCard) =>
        table.Cards.Any(card => card.FaceCard == faceCard);
}