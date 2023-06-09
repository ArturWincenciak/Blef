using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests;

// todo: unit tests: IsNotPokerHandOnTableTests

public class IsOnTableTests
{
    [Theory]
    [ClassData(typeof(PokerHandCases))]
    internal void PokerHandIsOnTableTests(Table table, PokerHand pokerHand)
    {
        // act
        var actual = pokerHand.IsOnTable(table);

        // assert
        Assert.True(actual);
    }
}