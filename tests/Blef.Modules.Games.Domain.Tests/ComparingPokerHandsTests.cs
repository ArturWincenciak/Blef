﻿using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class ComparingPokerHandsTests
{
    [Fact]
    public void HighCardIsBetterThenOtherHighCardTests()
    {
        IsBetterThen(FaceCard.Ace, FaceCard.King);
        IsBetterThen(FaceCard.King, FaceCard.Queen);
        IsBetterThen(FaceCard.Queen, FaceCard.Jack);
        IsBetterThen(FaceCard.Jack, FaceCard.Ten);
        IsBetterThen(FaceCard.Ten, FaceCard.Nine);
        IsBetterThen(FaceCard.Ace, FaceCard.Nine);
        IsBetterThen(FaceCard.King, FaceCard.Ten);

        void IsBetterThen(FaceCard higher, FaceCard lower)
        {
            // arrange
            var higherHighCard = HighCard.Create(higher.ToString());
            var lowerHighCard = HighCard.Create(lower.ToString());

            // act
            var actual = higherHighCard.IsBetterThan(lowerHighCard);

            // assert
            Assert.True(actual);
        }
    }

    [Fact]
    public void PairIsBetterThenOtherPairTests()
    {
        IsBetterThen(FaceCard.Ace, FaceCard.King);
        IsBetterThen(FaceCard.King, FaceCard.Queen);
        IsBetterThen(FaceCard.Queen, FaceCard.Jack);
        IsBetterThen(FaceCard.Jack, FaceCard.Ten);
        IsBetterThen(FaceCard.Ten, FaceCard.Nine);
        IsBetterThen(FaceCard.Ace, FaceCard.Nine);
        IsBetterThen(FaceCard.King, FaceCard.Ten);

        void IsBetterThen(FaceCard higher, FaceCard lower)
        {
            // arrange
            var higherPair = Pair.Create(higher.ToString());
            var lowerPair = Pair.Create(lower.ToString());

            // act
            var actual = higherPair.IsBetterThan(lowerPair);

            // assert
            Assert.True(actual);
        }
    }

    [Fact]
    public void TwoPairsIsBetterThenOtherTwoPairsTests()
    {
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.King), lower: (FaceCard.King, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Queen), lower: (FaceCard.Jack, FaceCard.Ten));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Jack), lower: (FaceCard.Ten, FaceCard.Nine));

        IsBetterThen(higher: (FaceCard.King, FaceCard.Ace), lower: (FaceCard.King, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.King), lower: (FaceCard.Jack, FaceCard.Ten));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Queen), lower: (FaceCard.Ten, FaceCard.Nine));

        IsBetterThen(higher: (FaceCard.Ace, FaceCard.King), lower: (FaceCard.Queen, FaceCard.King));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Queen), lower: (FaceCard.Ten, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Jack), lower: (FaceCard.Nine, FaceCard.Ten));

        IsBetterThen(higher: (FaceCard.King, FaceCard.Ace), lower: (FaceCard.Queen, FaceCard.King));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.King), lower: (FaceCard.Ten, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Queen), lower: (FaceCard.Nine, FaceCard.Ten));

        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.King, FaceCard.Ten));
        IsBetterThen(higher: (FaceCard.Nine, FaceCard.Ace), lower: (FaceCard.King, FaceCard.Ten));
        IsBetterThen(higher: (FaceCard.Nine, FaceCard.Ace), lower: (FaceCard.Ten, FaceCard.King));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.King));

        IsBetterThen(higher: (FaceCard.Ten, FaceCard.King), lower: (FaceCard.Queen, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ten), lower: (FaceCard.Queen, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ten), lower: (FaceCard.Jack, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.King), lower: (FaceCard.Jack, FaceCard.Queen));

        IsBetterThen(higher: (FaceCard.Ace, FaceCard.King), lower: (FaceCard.Ace, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ace), lower: (FaceCard.Ace, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ace), lower: (FaceCard.Queen, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.King), lower: (FaceCard.Queen, FaceCard.Ace));

        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Ten), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Jack), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Jack), lower: (FaceCard.Nine, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Ten), lower: (FaceCard.Nine, FaceCard.Jack));

        void IsBetterThen((FaceCard First, FaceCard Second) higher, (FaceCard First, FaceCard Second) lower)
        {
            // arrange
            var higherTwoPairs = TwoPairs.Create($"{higher.First},{higher.Second}");
            var lowerTwoPairs = TwoPairs.Create($"{lower.First},{lower.Second}");

            // act
            var actual = higherTwoPairs.IsBetterThan(lowerTwoPairs);

            // assert
            Assert.True(actual);
        }
    }

    [Fact]
    public void PairIsBetterThenHighCardTests()
    {
        // arrange
        var pair = Pair.Create(FaceCard.Nine.ToString());
        var highCard = HighCard.Create(FaceCard.Ace.ToString());

        // act
        var actual = pair.IsBetterThan(highCard);

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void TwoPairsIsBetterThenHighCardTests()
    {

    }

    [Fact]
    public void TwoPairsIsBetterThenPairTests()
    {

    }

    [Fact]
    public void LowStraightIsBetterThenHighCardTests()
    {

    }

    [Fact]
    public void LowStraightIsBetterThenPairTests()
    {

    }

    [Fact]
    public void LowStraightIsBetterThenTwoPairsTests()
    {

    }

    [Fact]
    public void HighStraightIsBetterThenHighCardTests()
    {

    }

    [Fact]
    public void HighStraightIsBetterThenPairTests()
    {

    }

    [Fact]
    public void HighStraightIsBetterThenTwoPairsTests()
    {

    }

    [Fact]
    public void HighStraightIsBetterThenLowStraightTests()
    {
        var highStraight = HighStraight.Create();
        var lowStraight = LowStraight.Create();
        Assert.True(highStraight.IsBetterThan(lowStraight));
    }
}