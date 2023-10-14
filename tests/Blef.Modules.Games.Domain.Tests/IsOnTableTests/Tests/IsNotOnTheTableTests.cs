using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class IsNotOnTheTableTests
{
    [Theory]
    [MemberData(nameof(GivenPokerHandTestCases))]
    internal void PokerHandIsNotOnTableTests(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.False(actual);
    }

    private static IEnumerable<object[]> Init =>
        new List<object[]>();

    public static IEnumerable<object[]> GivenPokerHandTestCases() =>
        Init
            .Concat(HighCardIsNotOnTheTableCases.Cases)
            .Concat(PairIsNotOnTheTableCases.Cases)
            .Concat(TwoPairsAreNotOnTheTableCases.Cases)
            .Concat(LowStraightIsNotOnTheTableCases.Cases)
            .Concat(HighStraightIsNotOnTheTableCases.Cases)
            .Concat(ThreeOfAKindIsNotOnTheTableCases.Cases)
            .Concat(FullHouseIsNotOnTheTableCases.Cases)
            .Concat(FlushIsNotOnTheTableCases.Cases)
            .Concat(FourOfAKindIsNotOnTheTableCases.Cases)
            .Concat(StraightFlushIsNotOnTheTableCases.Cases)
            .Concat(RoyalFlushIsNotOnTheTableCases.Cases);
}