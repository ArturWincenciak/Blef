using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class MapPokerHandTest
{
    [Fact]
    internal void MapPokerHandTest_WhenUnknownTypeOfPokerHand_ShouldThrowArgumentOutOfRangeException()
    {
        // arrange
        const string unknownBid = "unknown:ace";

        // act, assert
        Assert.Throws<ArgumentOutOfRangeException>(() => PokerHand.Map(unknownBid));
    }
}