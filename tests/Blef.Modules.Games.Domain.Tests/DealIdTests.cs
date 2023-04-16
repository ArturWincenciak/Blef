using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealIdTests
{
    [Fact]
    public void CreateDealIdTest()
    {
        // arrange
        var guid = Guid.Parse("ED665A22-74CF-4466-80FF-9BC005889054");
        var gameId = new GameId(guid);
        var dealNumber = new DealNumber(1);

        // act
        var actual = new DealId(gameId, dealNumber);

        // assert
        Assert.True(new GameId(guid) == actual.GameId);
        Assert.True(new DealNumber(1) == actual.Number);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealId(null, null));

    [Fact]
    public void CannotCreateWithNullGameIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealId(null, new DealNumber(1)));

    [Fact]
    public void CannotCreateWithNullDealNumberArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var guid = Guid.Parse("458C41E2-C477-4918-9353-96243FEFF99F");
            var gameId = new GameId(guid);
            return new DealId(gameId, null);
        });
}