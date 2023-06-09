using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class IsNotOnTheTableTests
{
    [Theory]
    [ClassData(typeof(PokerHandIsNotOnTheTableCases))]
    internal void PokerHandIsNotOnTableTests(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.False(actual);
    }
}