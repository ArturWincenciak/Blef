using Blef.Modules.Games.Domain.Entities.PokerHands;

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
}