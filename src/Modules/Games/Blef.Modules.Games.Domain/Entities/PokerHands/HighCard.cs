namespace Blef.Modules.Games.Domain.Entities.PokerHands
{
    public class HighCard : PokerHand
    {
        private readonly FaceCard _faceCard;

        public HighCard(FaceCard faceCard)
        {
            _faceCard = faceCard;
        }

        public override bool IsOnTable(IReadOnlyCollection<Card> table)
        {
            return table.Any(x => x.FaceCard == _faceCard);
        }

        protected override int PokerHandRank => 1;

        protected override int GetInnerRank()
        {
            return (int)_faceCard;
        }
    }
}