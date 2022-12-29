namespace Blef.Modules.Games.Domain.Entities.PokerHands;

public class HighCard : PokerHand
{
    private readonly FaceCard _faceCard;

    public HighCard(FaceCard faceCard) =>
        _faceCard = faceCard;

    protected override int PokerHandRank => 1;

    public override bool IsOnTable(IReadOnlyCollection<Card> table) =>
        table.Any(x => x.FaceCard == _faceCard);

    protected override int GetInnerRank() =>
        (int) _faceCard;
}