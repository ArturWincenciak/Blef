using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealNumberTests
{
    [Fact]
    public void CrateDealNumberTest()
    {
        var exception = Record.Exception(() =>
        {
            var dealNumber = new DealNumber(1);
            Assert.Equal(expected: 1, actual: dealNumber.Number);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void NumberCannotBeLessThanOneTest() =>
        Assert.Throws<ArgumentException>(() => new DealNumber(0));
}