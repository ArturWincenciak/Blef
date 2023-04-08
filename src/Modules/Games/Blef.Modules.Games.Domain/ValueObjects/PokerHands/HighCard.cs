using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal class HighCard : PokerHand
{
    public const string Type = "high-card";
    private readonly FaceCard _faceCard;

    protected override int PokerHandRank => 1;

    public HighCard(FaceCard faceCard) =>
        _faceCard = faceCard;

    public override bool IsOnTable(Table table) =>
        table.Contains(_faceCard);

    protected override int GetInnerRank() =>
        (int) _faceCard;

    public override string Serialize() =>
        $"{Type}:{_faceCard.ToString().ToLower()}";
}