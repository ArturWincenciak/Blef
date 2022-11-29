namespace Blef.Modules.Games.Domain.Entities.PokerHands
{
    public class HighStraight : PokerHand
    {
        public override bool IsOnTable(IReadOnlyCollection<Card> table)
        {
            return table.HasFaceCard(FaceCard.Ten) &&
                   table.HasFaceCard(FaceCard.Jack) &&
                   table.HasFaceCard(FaceCard.Queen) &&
                   table.HasFaceCard(FaceCard.King) &&
                   table.HasFaceCard(FaceCard.Ace);
        }

        protected override int PokerHandRank => 5;

        protected override int GetInnerRank()
        {
            // It is not important for this kind of PokerHand
            return 0;
        }
    }
}