namespace Blef.Modules.Games.Domain.Entities.PokerHands
{
    public class LowStraight : PokerHand
    {
        public override bool IsOnTable(IReadOnlyCollection<Card> table)
        {
            return table.HasFaceCard(FaceCard.Nine) &&
                   table.HasFaceCard(FaceCard.Ten) &&
                   table.HasFaceCard(FaceCard.Jack) &&
                   table.HasFaceCard(FaceCard.Queen) &&
                   table.HasFaceCard(FaceCard.King);
        }

        protected override int PokerHandRank => 4;

        protected override int GetInnerRank()
        {
            // It is not important for this kind of PokerHand
            return 0;
        }
    }
}