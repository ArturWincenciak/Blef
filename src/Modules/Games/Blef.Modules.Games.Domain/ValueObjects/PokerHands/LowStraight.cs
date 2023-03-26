using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal class LowStraight : PokerHand
{
    public const string Type = "low-straight";
    protected override int PokerHandRank => 4;

    public override bool IsOnTable(IReadOnlyCollection<Card> table) =>
        table.HasFaceCard(FaceCard.Nine) &&
        table.HasFaceCard(FaceCard.Ten) &&
        table.HasFaceCard(FaceCard.Jack) &&
        table.HasFaceCard(FaceCard.Queen) &&
        table.HasFaceCard(FaceCard.King);

    protected override int GetInnerRank() =>
        0; // It is not important for this kind of PokerHand

    public override string Serialize() =>
        Type;
}