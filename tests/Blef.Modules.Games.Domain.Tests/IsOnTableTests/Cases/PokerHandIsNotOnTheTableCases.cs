using System.Collections;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public class PokerHandIsNotOnTheTableCases : IEnumerable<object[]>
{
    private static IEnumerable<object[]> Init =>
        new List<object[]>();

    public IEnumerator<object[]> GetEnumerator() =>
        GivenPokerHandTestCases().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private static IEnumerable<object[]> GivenPokerHandTestCases() =>
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