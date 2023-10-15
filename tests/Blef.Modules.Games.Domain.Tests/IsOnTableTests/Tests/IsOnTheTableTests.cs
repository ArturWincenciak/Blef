using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class IsOnTheTableTests
{
    private static IEnumerable<object[]> Init =>
        new List<object[]>();

    [Theory]
    [MemberData(nameof(GivenPokerHandTestCases))]
    internal void PokerHandIsOnTableTests(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.True(actual);
    }

    public static IEnumerable<object[]> GivenPokerHandTestCases() =>
        Init
            .Concat(HighCardIsOnTheTableCases.Cases)
            .Concat(PairIsOnTheTableCases.Cases)
            .Concat(TwoPairsAreOnTheTableCases.Cases)
            .Concat(LowStraightIsOnTheTableCases.Cases)
            .Concat(HighStraightIsOnTheTableCases.Cases)
            .Concat(ThreeOfAKindIsOnTheTableCases.Cases)
            .Concat(FullHouseIsOnTheTableCases.Cases)
            .Concat(FlushIsOnTheTableCases.Cases)
            .Concat(FourOfAKindIsOnTheTableCases.Cases)
            .Concat(StraightFlushIsOnTheTableCases.Cases)
            .Concat(RoyalFlushIsOnTheTableCases.Cases);
}