namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

public class HighStraight : PokerHand
{
    protected override int PokerHandRank => 5;

    public override bool IsOnTable(IReadOnlyCollection<Card> table) =>
        table.HasFaceCard(FaceCard.Ten) &&
        table.HasFaceCard(FaceCard.Jack) &&
        table.HasFaceCard(FaceCard.Queen) &&
        table.HasFaceCard(FaceCard.King) &&
        table.HasFaceCard(FaceCard.Ace);

    protected override int GetInnerRank() =>
        0; // It is not important for this kind of PokerHand
}