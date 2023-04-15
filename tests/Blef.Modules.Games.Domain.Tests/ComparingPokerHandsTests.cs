using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects;
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