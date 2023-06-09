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
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Jack, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Queen, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.King, FaceCard.Nine), lower: (FaceCard.Queen, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Nine, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Ten, FaceCard.Ace));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.Queen, FaceCard.Nine));
        IsBetterThen(higher: (FaceCard.Ace, FaceCard.Nine), lower: (FaceCard.King, FaceCard.Ace));

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
                higherPokerHand: () => FullHouse.Create($"{higher.ThreeOfAKind},{higher.Pair}"),
                lowerPokerHand: () => FullHouse.Create($"{lower.TreeOfAKind},{lower.Pair}"));
    }

    [Fact]
    public void FlushIsBetterThenOtherFlushTests()
    {
        IsBetterThen(Suit.Spades, Suit.Hearts);
        IsBetterThen(Suit.Spades, Suit.Diamonds);
        IsBetterThen(Suit.Spades, Suit.Clubs);
        IsBetterThen(Suit.Hearts, Suit.Diamonds);
        IsBetterThen(Suit.Hearts, Suit.Clubs);
        IsBetterThen(Suit.Diamonds, Suit.Clubs);

        void IsBetterThen(Suit higher, Suit lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => Flush.Create(higher.ToString()),
                lowerPokerHand: () => Flush.Create(lower.ToString()));
    }

    [Fact]
    public void FourOfAKindIsBetterThenOtherFourOfKind()
    {
        IsBetterThen(FaceCard.Ace, FaceCard.King);
        IsBetterThen(FaceCard.Ace, FaceCard.Queen);
        IsBetterThen(FaceCard.Ace, FaceCard.Jack);
        IsBetterThen(FaceCard.Ace, FaceCard.Ten);
        IsBetterThen(FaceCard.Ace, FaceCard.Nine);
        IsBetterThen(FaceCard.King, FaceCard.Queen);
        IsBetterThen(FaceCard.King, FaceCard.Jack);
        IsBetterThen(FaceCard.King, FaceCard.Ten);
        IsBetterThen(FaceCard.King, FaceCard.Nine);
        IsBetterThen(FaceCard.Queen, FaceCard.Jack);
        IsBetterThen(FaceCard.Queen, FaceCard.Ten);
        IsBetterThen(FaceCard.Queen, FaceCard.Nine);
        IsBetterThen(FaceCard.Jack, FaceCard.Ten);
        IsBetterThen(FaceCard.Jack, FaceCard.Nine);
        IsBetterThen(FaceCard.Ten, FaceCard.Nine);

        void IsBetterThen(FaceCard higher, FaceCard lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => FourOfAKind.Create(higher.ToString()),
                lowerPokerHand: () => FourOfAKind.Create(lower.ToString()));
    }

    [Fact]
    public void StraightFlushIsBetterThenOtherStraightFlush()
    {
        IsBetterThen(Suit.Spades, Suit.Hearts);
        IsBetterThen(Suit.Spades, Suit.Diamonds);
        IsBetterThen(Suit.Spades, Suit.Clubs);
        IsBetterThen(Suit.Hearts, Suit.Diamonds);
        IsBetterThen(Suit.Hearts, Suit.Clubs);
        IsBetterThen(Suit.Diamonds, Suit.Clubs);

        void IsBetterThen(Suit higher, Suit lower) =>
            AssertThatIsBetterThan(
                higherPokerHand: () => StraightFlush.Create(higher.ToString()),
                lowerPokerHand: () => StraightFlush.Create(lower.ToString()));
    }

    [Fact]
    public void PairIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Pair.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void TwoPairsIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => TwoPairs.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void TwoPairsIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => TwoPairs.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void LowStraightIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            LowStraight.Create,
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void LowStraightIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            LowStraight.Create,
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void LowStraightIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            LowStraight.Create,
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void HighStraightIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            HighStraight.Create,
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void HighStraightIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            HighStraight.Create,
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void HighStraightIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            HighStraight.Create,
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
            LowStraight.Create);

    [Fact]
    public void ThreeOfAKindIsBetterThenHighStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => ThreeOfAKind.Create(FaceCard.Nine.ToString()),
            HighStraight.Create);

    [Fact]
    public void FullIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FullIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FullIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void FullIsBetterThenLowStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            LowStraight.Create);

    [Fact]
    public void FullIsBetterThenHighStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            HighStraight.Create);

    [Fact]
    public void FullIsBetterThenThreeOfAKindTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"),
            lowerPokerHand: () => ThreeOfAKind.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FlushIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FlushIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FlushIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void FlushIsBetterThenLowStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            LowStraight.Create);

    [Fact]
    public void FlushIsBetterThenHighStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            HighStraight.Create);

    [Fact]
    public void FlushIsBetterThenThreeOfAKindTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            lowerPokerHand: () => ThreeOfAKind.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FlushIsBetterThenFullTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => Flush.Create(Suit.Spades.ToString()),
            lowerPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"));

    [Fact]
    public void FourOfAKindIsBetterThenHighCardTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => HighCard.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FourOfAKindIsBetterThenPairTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => Pair.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FourOfAKindIsBetterThenTwoPairsTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => TwoPairs.Create($"{FaceCard.Ace},{FaceCard.King}"));

    [Fact]
    public void FourOfAKindIsBetterThenLowStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            LowStraight.Create);

    [Fact]
    public void FourOfAKindIsBetterThenHighStraightTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            HighStraight.Create);

    [Fact]
    public void FourOfAKindIsBetterThenThreeOfAKindTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => ThreeOfAKind.Create(FaceCard.Ace.ToString()));

    [Fact]
    public void FourOfAKindIsBetterThenFullTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => FullHouse.Create($"{FaceCard.Nine},{FaceCard.Ten}"));

    [Fact]
    public void FourOfAKindIsBetterThenFlushTests() =>
        AssertThatIsBetterThan(
            higherPokerHand: () => FourOfAKind.Create(FaceCard.Nine.ToString()),
            lowerPokerHand: () => Flush.Create(Suit.Spades.ToString()));
}
