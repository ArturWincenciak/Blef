namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

public class Pair : PokerHand
{
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 2;

    public Pair(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(IReadOnlyCollection<Card> table) =>
        table.Count(x => x.FaceCard == _faceCard) >= 2;

    protected override int GetInnerRank() =>
        (int) _faceCard;
}