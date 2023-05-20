namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class Pair : PokerHand
{
    public const string TYPE = "pair";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 2;

    private Pair(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(Table table) =>
        table.Count(_faceCard) >= 2;

    protected override int GetInnerRank() =>
        _faceCard.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_faceCard.ToString().ToLower()}";

    public static PokerHand Create(string faceCard) =>
        new Pair(FaceCard.Create(faceCard));
}