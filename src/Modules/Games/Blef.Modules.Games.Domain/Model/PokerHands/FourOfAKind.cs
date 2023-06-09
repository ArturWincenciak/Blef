namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class FourOfAKind : PokerHand
{
    public const string TYPE = "four-of-a-kind";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 9;

    private FourOfAKind(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(Table table) =>
        table.Count(_faceCard) == 4;

    protected override int GetInnerRank() =>
        _faceCard.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_faceCard.ToString().ToLower()}";

    public static PokerHand Create(string faceCard) =>
        new FourOfAKind(FaceCard.Create(faceCard));
}
