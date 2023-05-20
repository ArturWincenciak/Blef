namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal class HighCard : PokerHand
{
    public const string TYPE = "high-card";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 1;

    private HighCard(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(Table table) =>
        table.Contains(_faceCard);

    protected override int GetInnerRank() =>
        _faceCard.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_faceCard.ToString().ToLower()}";

    public static PokerHand Create(string faceCard) =>
        new HighCard(FaceCard.Create(faceCard));
}