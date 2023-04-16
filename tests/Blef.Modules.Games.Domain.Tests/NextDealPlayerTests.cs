using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class NextDealPlayerTests
{
    [Fact]
    public void CreateNextDealPlayerTest()
    {
        var exception = Record.Exception(() =>
        {
            // arrange
            var guid = Guid.Parse("0EA48031-AE45-4033-AFCC-8C56D8D66F65");
            var playerId = new PlayerId(guid);
            var cardsAmount = CardsAmount.Initial;

            // act
            var actual = new NextDealPlayer(playerId, cardsAmount);

            // assert
            Assert.Equal(expected: playerId, actual: actual.PlayerId);
            Assert.Equal(expected: cardsAmount, actual: actual.CardsAmount);
        });

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
            var guid = Guid.Parse("D1954AA3-45C1-4F90-8638-41A491521FF1");
            var playerId = new PlayerId(guid);
            return new NextDealPlayer(playerId, null);
        });
}