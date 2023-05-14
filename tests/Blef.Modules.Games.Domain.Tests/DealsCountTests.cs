using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealsCountTests
{
    [Fact]
    public void CreateCountTest()
    {
        // arrange
        int count = 10;

        // act
        var actual = DealsCount.Create(count);

        // assert
        Assert.NotNull(actual);
    }

    [Fact]
    public void CountCannotBeNegativeTest()
    {
        // arrange
        int count = -5;

        // act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            DealsCount.Create(count));
    }

    [Fact]
    public void ModuloTest()
    {
        // arrange
        var dealsCount = DealsCount.Create(10);
        var playersCount = PlayersCount.Create(3);

        // act
        var actual = dealsCount % playersCount;
        var expected = 1;

        // assert
        Assert.Equal(expected, actual);
    }
}