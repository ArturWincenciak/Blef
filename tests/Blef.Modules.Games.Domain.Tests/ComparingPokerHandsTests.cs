using Blef.Modules.Games.Domain.ValueObjects.Cards;
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
            var higherHighCard = HighCard.Deserialize(higher.ToString());
            var lowerHighCard = HighCard.Deserialize(lower.ToString());
            Assert.True(higherHighCard.IsBetterThan(lowerHighCard));
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
            var higherPair = Pair.Deserialize(higher.ToString());
            var lowerPair = Pair.Deserialize(lower.ToString());
            Assert.True(higherPair.IsBetterThan(lowerPair));
        }
    }

    [Fact]
    public void TwoPairsIsBetterThenOtherTwoPairsTests()
    {
        IsBetterThen((FaceCard.Ace, FaceCard.King), (FaceCard.King, FaceCard.Queen));
        IsBetterThen((FaceCard.King, FaceCard.Queen), (FaceCard.Jack, FaceCard.Ten));
        IsBetterThen((FaceCard.Queen, FaceCard.Jack), (FaceCard.Ten, FaceCard.Nine));

        IsBetterThen((FaceCard.King, FaceCard.Ace), (FaceCard.King, FaceCard.Queen));
        IsBetterThen((FaceCard.Queen, FaceCard.King), (FaceCard.Jack, FaceCard.Ten));
        IsBetterThen((FaceCard.Jack, FaceCard.Queen), (FaceCard.Ten, FaceCard.Nine));

        IsBetterThen((FaceCard.Ace, FaceCard.King), (FaceCard.Queen, FaceCard.King));
        IsBetterThen((FaceCard.King, FaceCard.Queen), (FaceCard.Ten, FaceCard.Jack));
        IsBetterThen((FaceCard.Queen, FaceCard.Jack), (FaceCard.Nine, FaceCard.Ten));

        IsBetterThen((FaceCard.King, FaceCard.Ace), (FaceCard.Queen, FaceCard.King));
        IsBetterThen((FaceCard.Queen, FaceCard.King), (FaceCard.Ten, FaceCard.Jack));
        IsBetterThen((FaceCard.Jack, FaceCard.Queen), (FaceCard.Nine, FaceCard.Ten));

        IsBetterThen((FaceCard.Ace, FaceCard.Nine), (FaceCard.King, FaceCard.Ten));
        IsBetterThen((FaceCard.Nine, FaceCard.Ace), (FaceCard.King, FaceCard.Ten));
        IsBetterThen((FaceCard.Nine, FaceCard.Ace), (FaceCard.Ten, FaceCard.King));
        IsBetterThen((FaceCard.Ace, FaceCard.Nine), (FaceCard.Ten, FaceCard.King));

        IsBetterThen((FaceCard.Ten, FaceCard.King), (FaceCard.Queen, FaceCard.Jack));
        IsBetterThen((FaceCard.King, FaceCard.Ten), (FaceCard.Queen, FaceCard.Jack));
        IsBetterThen((FaceCard.King, FaceCard.Ten), (FaceCard.Jack, FaceCard.Queen));
        IsBetterThen((FaceCard.Ten, FaceCard.King), (FaceCard.Jack, FaceCard.Queen));

        IsBetterThen((FaceCard.Ace, FaceCard.King), (FaceCard.Ace, FaceCard.Queen));
        IsBetterThen((FaceCard.King, FaceCard.Ace), (FaceCard.Ace, FaceCard.Queen));
        IsBetterThen((FaceCard.King, FaceCard.Ace), (FaceCard.Queen, FaceCard.Ace));
        IsBetterThen((FaceCard.Ace, FaceCard.King), (FaceCard.Queen, FaceCard.Ace));

        IsBetterThen((FaceCard.Jack, FaceCard.Ten), (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen((FaceCard.Ten, FaceCard.Jack), (FaceCard.Jack, FaceCard.Nine));
        IsBetterThen((FaceCard.Ten, FaceCard.Jack), (FaceCard.Nine, FaceCard.Jack));
        IsBetterThen((FaceCard.Jack, FaceCard.Ten), (FaceCard.Nine, FaceCard.Jack));

        void IsBetterThen((FaceCard First, FaceCard Second) higher, (FaceCard First, FaceCard Second) lower)
        {
            var higherTwoPairs = TwoPairs.Deserialize($"{higher.First},{higher.Second}");
            var lowerTwoPairs = TwoPairs.Deserialize($"{lower.First},{lower.Second}");
            Assert.True(higherTwoPairs.IsBetterThan(lowerTwoPairs));
        }
    }

    [Fact]
    public void HighStraightIsBetterThenLowStraightTests()
    {
        var highStraight = HighStraight.Create();
        var lowStraight = LowStraight.Create();
        Assert.True(highStraight.IsBetterThan(lowStraight));
    }
}