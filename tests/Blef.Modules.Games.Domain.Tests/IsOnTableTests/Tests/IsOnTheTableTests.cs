using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class IsOnTheTableTests
{
    [Theory]
    [ClassData(typeof(HighCardIsOnTheTableCases))]
    [ClassData(typeof(PairIsOnTheTableCases))]
    [ClassData(typeof(TwoPairsAreOnTheTableCases))]
    [ClassData(typeof(LowStraightIsOnTheTableCases))]
    [ClassData(typeof(HighStraightIsOnTheTableCases))]
    [ClassData(typeof(ThreeOfAKindIsOnTheTableCases))]
    [ClassData(typeof(FullHouseIsOnTheTableCases))]
    [ClassData(typeof(FlushIsOnTheTableCases))]
    [ClassData(typeof(FourOfAKindIsOnTheTableCases))]
    [ClassData(typeof(StraightFlushIsOnTheTableCases))]
    [ClassData(typeof(RoyalFlushIsOnTheTableCases))]
    internal void PokerHandIsOnTableTests(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.True(actual);
    }
}