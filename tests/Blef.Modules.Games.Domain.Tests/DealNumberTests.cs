using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealNumberTests
{
    [Fact]
    public void CrateDealNumberTest()
    {
        var exception = Record.Exception(() =>
        {
            // arrange
            var number = 1;

            // act
            var dealNumber = new DealNumber(number);

            // assert
            Assert.Equal(expected: number, actual: dealNumber.Number);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void NumberCannotBeLessThanOneTest() =>
        Assert.Throws<ArgumentException>(() => new DealNumber(0));
}