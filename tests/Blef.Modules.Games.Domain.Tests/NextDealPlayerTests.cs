using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class NextDealPlayerTests
{
    [Fact]
    public void CreateNextDealPlayerTest()
    {
        // arrange
        var playerId = new PlayerId(Guid.NewGuid());
        var cardsAmount = CardsAmount.Initial;

        // act
        var exception = Record.Exception(() => new NextDealPlayer(playerId, cardsAmount));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new NextDealPlayer(null, null));

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var cardsAmount = CardsAmount.Initial;
            return new NextDealPlayer(null, cardsAmount);
        });

    [Fact]
    public void CannotCreateWithNullCardAmountArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var playerId = new PlayerId(Guid.NewGuid());
            return new NextDealPlayer(playerId, null);
        });
}