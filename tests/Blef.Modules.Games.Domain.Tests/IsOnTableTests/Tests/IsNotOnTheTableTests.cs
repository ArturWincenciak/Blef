using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class IsNotOnTheTableTests
{
    [Theory]
    [ClassData(typeof(HighCardIsNotOnTheTableCases))]
    [ClassData(typeof(PairIsNotOnTheTableCases))]
    [ClassData(typeof(TwoPairsAreNotOnTheTableCases))]
    [ClassData(typeof(LowStraightIsNotOnTheTableCases))]
    [ClassData(typeof(HighStraightIsNotOnTheTableCases))]
    [ClassData(typeof(ThreeOfAKindIsNotOnTheTableCases))]
    [ClassData(typeof(FullHouseIsNotOnTheTableCases))]
    [ClassData(typeof(FourOfAKindIsNotOnTheTableCases))]
    [ClassData(typeof(StraightFlushIsNotOnTheTableCases))]
    [ClassData(typeof(RoyalFlushIsNotOnTheTableCases))]
    [ClassData(typeof(FlushIsNotOnTheTableCases))]
    internal void PokerHandIsNotOnTableTests(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.False(actual);
    }
}