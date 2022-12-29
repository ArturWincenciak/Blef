namespace Blef.Modules.Games.Domain.Entities.PokerHands;

public class LowStraight : PokerHand
{
    protected override int PokerHandRank => 4;

    public override bool IsOnTable(IReadOnlyCollection<Card> table) =>
        table.HasFaceCard(FaceCard.Nine) &&
        table.HasFaceCard(FaceCard.Ten) &&
        table.HasFaceCard(FaceCard.Jack) &&
        table.HasFaceCard(FaceCard.Queen) &&
        table.HasFaceCard(FaceCard.King);

    protected override int GetInnerRank() =>
        0; // It is not important for this kind of PokerHand
}