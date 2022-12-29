namespace Blef.Modules.Games.Domain.Entities.PokerHands;

public class Pair : PokerHand
{
    private readonly FaceCard _faceCard;

    public Pair(FaceCard faceCard) =>
        _faceCard = faceCard;

    protected override int PokerHandRank => 2;

    public override bool IsOnTable(IReadOnlyCollection<Card> table) =>
        table.Count(x => x.FaceCard == _faceCard) >= 2;

    protected override int GetInnerRank() =>
        (int) _faceCard;
}