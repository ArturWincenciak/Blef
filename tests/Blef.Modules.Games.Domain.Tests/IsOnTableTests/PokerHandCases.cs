using System.Collections;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests;

public class PokerHandCases : IEnumerable<object[]>
{
    private static IEnumerable<object[]> Init =>
        new List<object[]>();

    public IEnumerator<object[]> GetEnumerator() =>
        GivenPokerHandTestCases().GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    private static IEnumerable<object[]> GivenPokerHandTestCases() =>
        Init
            .Concat(HighCardCases.TableWithPokerHand)
            .Concat(PairCases.TableWithPokerHand)
            .Concat(TwoPairsCases.TableWithPokerHand)
            .Concat(LowStraightCases.TableWithPokerHand)
            .Concat(HighStraightCases.TableWithPokerHand)
            .Concat(ThreeOfAKindCases.TableWithPokerHand)
            .Concat(FullHouseCases.TableWithPokerHand)
            .Concat(FlushCases.TableWithPokerHand)
            .Concat(FourOfAKindCases.TableWithPokerHand);
}