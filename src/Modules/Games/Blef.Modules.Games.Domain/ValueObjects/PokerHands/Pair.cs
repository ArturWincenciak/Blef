using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal sealed class Pair : PokerHand
{
    public const string Type = "pair";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 2;

    public Pair(FaceCard faceCard)
    {
        if (faceCard is FaceCard.None) // todo: exception
            throw new ArgumentException("TBD");

        _faceCard = faceCard;
    }

    public override bool IsOnTable(Table table) =>
        table.Count(_faceCard) >= 2;

    protected override int GetInnerRank() =>
        (int) _faceCard;

    public override string Serialize() =>
        $"{Type}:{_faceCard.ToString().ToLower()}";
}