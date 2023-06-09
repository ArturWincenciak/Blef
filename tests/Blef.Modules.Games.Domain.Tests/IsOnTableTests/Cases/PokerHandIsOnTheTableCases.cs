using System.Collections;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public class PokerHandIsOnTheTableCases : IEnumerable<object[]>
{
    private static IEnumerable<object[]> Init =>
        new List<object[]>();

    public IEnumerator<object[]> GetEnumerator() =>
        GivenPokerHandTestCases().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private static IEnumerable<object[]> GivenPokerHandTestCases() =>
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