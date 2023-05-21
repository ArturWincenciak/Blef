using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class ComparingPokerHandsTests
{
    private static void AssertThatIsBetterThan(Func<PokerHand> higherPokerHand, Func<PokerHand> lowerPokerHand)
    {
        // arrange
        var higher = higherPokerHand();
        var lower = lowerPokerHand();

        // act
        var actual = higher.IsBetterThan(lower);

        // assert
        Assert.True(actual);
    }

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

        void IsBetterThen(FaceCard higher, FaceCard lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => HighCard.Create(higher.ToString()),
                lowerPokerHand: () => HighCard.Create(lower.ToString()));
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

        void IsBetterThen(FaceCard higher, FaceCard lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => Pair.Create(higher.ToString()),
                lowerPokerHand: () => Pair.Create(lower.ToString()));
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

        void IsBetterThen((FaceCard First, FaceCard Second) higher, (FaceCard First, FaceCard Second) lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => TwoPairs.Create($"{higher.First},{higher.Second}"),
                lowerPokerHand: () => TwoPairs.Create($"{lower.First},{lower.Second}"));
    }

    [Fact]
    public void ThreeOfAKindIsBetterThenOtherThreeOfAKindTests()
    {
        IsBetterThen(FaceCard.Ace, FaceCard.King);
        IsBetterThen(FaceCard.King, FaceCard.Queen);
        IsBetterThen(FaceCard.Queen, FaceCard.Jack);
        IsBetterThen(FaceCard.Jack, FaceCard.Ten);
        IsBetterThen(FaceCard.Ten, FaceCard.Nine);
        IsBetterThen(FaceCard.Ace, FaceCard.Nine);
        IsBetterThen(FaceCard.King, FaceCard.Ten);

        void IsBetterThen(FaceCard higher, FaceCard lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => ThreeOfAKind.Create(higher.ToString()),
                lowerPokerHand: () => ThreeOfAKind.Create(lower.ToString()));
    }

    [Fact]
    public void FullIsBetterThenOtherFullTests()
    {
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Nine), lower: (FaceCard.Queen, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Nine), lower: (FaceCard.King, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Nine), lower: (FaceCard.Ace, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.Queen, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.King, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.Ace, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.King, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Ace, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Queen, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Ace, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Queen, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.King, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Ace, FaceCard.Nine));

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
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.King));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ten), lower: (FaceCard.Queen, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ten), lower: (FaceCard.Jack, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.King), lower: (FaceCard.Ace, FaceCard.Queen));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Ace), lower: (FaceCard.Queen, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.King), lower: (FaceCard.Queen, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ten, FaceCard.Jack), lower: (FaceCard.Nine, FaceCard.Jack));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Ten), lower: (FaceCard.Nine, FaceCard.Jack));

        void IsBetterThen((FaceCard ThreeOfAKind, FaceCard Pair) higher, (FaceCard TreeOfAKind, FaceCard Pair) lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => TwoPairs.Create($"{higher.ThreeOfAKind},{higher.Pair}"),
                lowerPokerHand: () => TwoPairs.Create($"{lower.TreeOfAKind},{lower.Pair}"));
    }

    [Fact]
    public void PairIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Pair.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void TwoPairsIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () =>  TwoPairs.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void TwoPairsIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => TwoPairs.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void LowStraightIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: LowStraight.Create,
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void LowStraightIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: LowStraight.Create,
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void LowStraightIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: LowStraight.Create,
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void HighStraightIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: HighStraight.Create,
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void HighStraightIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: HighStraight.Create,
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void HighStraightIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: HighStraight.Create,
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void HighStraightIsBetterThenLowStraightTests() =>
        AssertThatIsBetterThan(
            HighStraight.Create,
            LowStraight.Create);

    [Fact]
    public void ThreeOfAKindIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => ThreeOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void ThreeOfAKindIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => ThreeOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void ThreeOfAKindIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => ThreeOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void ThreeOfAKindIsBetterThenLowStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => ThreeOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: LowStraight.Create);

    [Fact]
    public void ThreeOfAKindIsBetterThenHighStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => ThreeOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: HighStraight.Create);
}