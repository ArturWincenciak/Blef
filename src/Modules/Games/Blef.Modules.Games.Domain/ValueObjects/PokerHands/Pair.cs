using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal class Pair : PokerHand
{
    public const string Type = "pair";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 2;

    public Pair(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(Table table) =>
        table.Cards.Count(x => x.FaceCard == _faceCard) >= 2;

    protected override int GetInnerRank() =>
        (int) _faceCard;

    public override string Serialize() =>
        $"{Type}:{_faceCard.ToString().ToLower()}";
}