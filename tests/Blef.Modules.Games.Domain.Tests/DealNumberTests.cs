using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealNumberTests
{
    [Fact]
    public void CrateDealNumberTest()
    {
        // arrange
        const int number = 1;

        // act
        var actual = new DealNumber(number);

        // assert
        Assert.Equal(number, actual.Number);
        Assert.True(new DealNumber(number) == actual);
    }

    [Fact]
    public void NumberCannotBeLessThanOneTest() =>
        Assert.Throws<ArgumentException>(() => new DealNumber(0));
}