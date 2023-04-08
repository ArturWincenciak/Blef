﻿using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.ValueObjects.PokerHands;

internal sealed class LowStraight : PokerHand
{
    public const string Type = "low-straight";
    protected override int PokerHandRank => 4;

    public override bool IsOnTable(Table table) =>
        table.HasFaceCard(FaceCard.Nine) &&
        table.HasFaceCard(FaceCard.Ten) &&
        table.HasFaceCard(FaceCard.Jack) &&
        table.HasFaceCard(FaceCard.Queen) &&
        table.HasFaceCard(FaceCard.King);

    protected override int GetInnerRank() =>
        0; // It is not important for this kind of PokerHand

    public override string Serialize() =>
        Type;
}