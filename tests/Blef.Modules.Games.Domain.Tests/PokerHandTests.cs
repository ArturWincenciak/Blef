using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.PokerHands;

public class PokerHandTests
{
    [Fact]
    public void Pair_should_be_better_than_high_card_without_comparing_face_card()
    {
        var highCard = PokerHandParser.Parse("high-card:ace");
        var pair = PokerHandParser.Parse("pair:nine");
        Assert.True(pair.IsBetterThan(highCard));
    }

    [Fact]
    public void Two_Pairs_should_be_better_than_one_pair_without_comparing_face_card()
    {
        var pair = PokerHandParser.Parse("pair:ace");
        var twoPairs = PokerHandParser.Parse("two-pairs:ten,nine");
        Assert.True(twoPairs.IsBetterThan(pair));
    }

    [Fact]
    public void High_Straight_should_be_better_than_Low_Straight()
    {
        var lowStraight = PokerHandParser.Parse("low-straight");
        var highStraight = PokerHandParser.Parse("high-straight");
        Assert.True(highStraight.IsBetterThan(lowStraight));
    }
}