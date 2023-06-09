namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class RoyalFlush : PokerHand
{
    public const string TYPE = "royal-flush";
    private readonly Suit _suit;

    protected override int PokerHandRank => 11;

    private RoyalFlush(Suit suit) =>
        _suit = suit;

    public override bool IsOnTable(Table table) =>
        table.Contains(new Card(FaceCard.Ace, _suit)) &&
        table.Contains(new Card(FaceCard.King, _suit)) &&
        table.Contains(new Card(FaceCard.Queen, _suit)) &&
        table.Contains(new Card(FaceCard.Jack, _suit)) &&
        table.Contains(new Card(FaceCard.Ten, _suit));

    protected override int GetInnerRank() =>
        _suit.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_suit.ToString().ToLower()}";

    public static PokerHand Create(string faceCard) =>
        new RoyalFlush(Suit.Create(faceCard));
}