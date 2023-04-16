using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealNumberTests
{
    [Fact]
    public void CrateDealNumberTest()
    {
        // arrange
        var number = 1;

        // act
        var dealNumber = new DealNumber(number);

        // assert
        Assert.Equal(number, dealNumber.Number);
    }

    [Fact]
    public void NumberCannotBeLessThanOneTest() =>
        Assert.Throws<ArgumentException>(() => new DealNumber(0));
}