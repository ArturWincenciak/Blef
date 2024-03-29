﻿namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class HighStraight : PokerHand
{
    public const string TYPE = "high-straight";
    protected override int PokerHandRank => 5;

    private HighStraight()
    {
    }

    public static HighStraight Create() => new();

    public override bool IsOnTable(Table table) =>
        table.Contains(FaceCard.Ten) &&
        table.Contains(FaceCard.Jack) &&
        table.Contains(FaceCard.Queen) &&
        table.Contains(FaceCard.King) &&
        table.Contains(FaceCard.Ace);

    protected override int GetInnerRank() => 0;

    public override string Serialize() => TYPE;
}