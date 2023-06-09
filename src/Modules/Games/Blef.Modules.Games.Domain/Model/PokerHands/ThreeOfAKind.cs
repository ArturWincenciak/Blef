namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class ThreeOfAKind : PokerHand
{
    public const string TYPE = "three-of-a-kind";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 6;

    private ThreeOfAKind(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(Table table) =>
        table.Count(_faceCard) >= 3;

    protected override int GetInnerRank() =>
        _faceCard.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_faceCard.ToString().ToLower()}";

    public static PokerHand Create(string faceCard) =>
        new ThreeOfAKind(FaceCard.Create(faceCard));
}