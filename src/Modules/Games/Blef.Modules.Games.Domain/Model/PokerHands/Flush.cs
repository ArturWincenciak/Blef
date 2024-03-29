﻿namespace Blef.Modules.Games.Domain.Model.PokerHands;

internal sealed class Flush : PokerHand
{
    public const string TYPE = "flush";
    private readonly Suit _suit;

    protected override int PokerHandRank => 8;

    private Flush(Suit suit) =>
        _suit = suit;

    public override bool IsOnTable(Table table) =>
        table.Count(_suit) >= 5;

    protected override int GetInnerRank() =>
        _suit.GetRank();

    public override string Serialize() =>
        $"{TYPE}:{_suit.ToString().ToLower()}";

    public static PokerHand Create(string suit) =>
        new Flush(Suit.Create(suit));
}