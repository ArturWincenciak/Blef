﻿using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

internal static class PokerHandFactory
{
    public static PokerHand GivenHighCard(FaceCard faceCard) =>
        HighCard.Create(faceCard.ToString());

    public static PokerHand GivenPair(FaceCard faceCard) =>
        Pair.Create(faceCard.ToString());

    public static PokerHand GivenTwoPairs(FaceCard firstFaceCard, FaceCard secondFaceCard) =>
        TwoPairs.Create($"{firstFaceCard},{secondFaceCard}");
}