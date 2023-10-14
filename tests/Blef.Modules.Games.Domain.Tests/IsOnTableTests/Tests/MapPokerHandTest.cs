using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Tests;

public class MapPokerHandTest
{
    [Fact]
    internal void MapPokerHandTest_WhenUnknownTypeOfPokerHand_ShouldThrowArgumentOutOfRangeException()
    {
        // arrange
        const string bid = "unknown:ace";

        // act
        PokerHand Act() => PokerHand.Map(bid);

        // assert
        Assert.Throws<ArgumentOutOfRangeException>((Func<PokerHand>?) Act ?? throw new Exception());
    }
}