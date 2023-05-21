namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class FullHouse : PokerHand
{
    public const string TYPE = "full";
    private readonly FaceCard _threeOfAKind;
    private readonly FaceCard _pair;

    protected override int PokerHandRank => 7;

    private FullHouse(FaceCard threeOfAKind, FaceCard pair)
    {
        if(threeOfAKind.Equals(pair))
            throw new ArgumentException("Three of a kind and pair cannot be the same card");

        _threeOfAKind = threeOfAKind;
        _pair = pair;
    }

    public override bool IsOnTable(Table table) =>
        table.Count(_threeOfAKind) >= 3 && table.Count(_pair) >= 2;

    protected override int GetInnerRank() =>
        (10 * _threeOfAKind.GetRank()) + _pair.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_threeOfAKind.ToString().ToLower()},{_pair.ToString().ToLower()}";

    public static PokerHand Create(string faceCards)
    {
        var faceCardParts = faceCards.Split(",");
        return new FullHouse(
            threeOfAKind: FaceCard.Create(faceCardParts[0]),
            pair: FaceCard.Create(faceCardParts[1]));
    }
}