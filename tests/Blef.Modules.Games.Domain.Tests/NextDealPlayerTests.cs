using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class NextDealPlayerTests
{
    [Fact]
    public void CreateNextDealPlayerTest()
    {
        // arrange
        var guid = Guid.Parse("0EA48031-AE45-4033-AFCC-8C56D8D66F65");
        var playerId = new PlayerId(guid);
        var cardsAmount = CardsAmount.Initial;

        // act
        var actual = new NextDealPlayer(playerId, cardsAmount, 1);

        // assert
        Assert.True(new PlayerId(guid) == actual.Player);
        Assert.True(CardsAmount.Initial == actual.CardsAmount);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new NextDealPlayer(Player: null, CardsAmount: null, 1));

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var cardsAmount = CardsAmount.Initial;
            return new NextDealPlayer(Player: null, cardsAmount, 1);
        });

    [Fact]
    public void CannotCreateWithNullCardAmountArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var guid = Guid.Parse("D1954AA3-45C1-4F90-8638-41A491521FF1");
            var playerId = new PlayerId(guid);
            return new NextDealPlayer(playerId, CardsAmount: null, 1);
        });
}