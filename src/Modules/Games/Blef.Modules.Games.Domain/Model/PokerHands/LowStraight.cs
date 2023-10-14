namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class LowStraight : PokerHand
{
    public const string TYPE = "low-straight";
    protected override int PokerHandRank => 4;

    private LowStraight()
    {
    }

    public static LowStraight Create() => new();

    public override bool IsOnTable(Table table) =>
        table.Contains(FaceCard.Nine) &&
        table.Contains(FaceCard.Ten) &&
        table.Contains(FaceCard.Jack) &&
        table.Contains(FaceCard.Queen) &&
        table.Contains(FaceCard.King);

    protected override int GetInnerRank() => 0;

    public override string Serialize() => TYPE;
}