namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class StraightFlush : PokerHand
{
    public const string TYPE = "straight-flush";
    private readonly Suit _suit;

    protected override int PokerHandRank => 10;

    private StraightFlush(Suit suit) =>
        _suit = suit;

    public override bool IsOnTable(Table table) =>
        table.Contains(new Card(FaceCard.King, _suit)) &&
        table.Contains(new Card(FaceCard.Queen, _suit)) &&
        table.Contains(new Card(FaceCard.Jack, _suit)) &&
        table.Contains(new Card(FaceCard.Ten, _suit)) &&
        table.Contains(new Card(FaceCard.Nine, _suit));

    protected override int GetInnerRank() =>
        _suit.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_suit.ToString().ToLower()}";

    public static PokerHand Create(string faceCard) =>
        new StraightFlush(Suit.Create(faceCard));
}