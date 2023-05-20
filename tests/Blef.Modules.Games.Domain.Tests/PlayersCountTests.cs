using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class PlayersCountTests
{
    [Fact]
    public void CreateCountTest()
    {
        // arrange
        const int count = 4;

        // act
        var actual = PlayersCount.Create(count);

        // assert
        Assert.NotNull(actual);
    }

    [Fact]
    public void CountCannotBeLessThanMinNumberOfPlayer()
    {
        // arrange
        const int count = 1;

        // act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            PlayersCount.Create(count));
    }

    [Fact]
    public void CountCannotBeMoreThanMaxNumberOfPlayer()
    {
        // arrange
        const int count = 5;

        // act & assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            PlayersCount.Create(count));
    }

    [Fact]
    public void AddOperatorTest()
    {
        // arrange
        var playersCount = PlayersCount.Create(3);
        const int intValue = 5;

        // act
        var actual = playersCount + intValue;
        const int expected = 8;

        // assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void CastToIntTest()
    {
        // arrange
        var playersCount = PlayersCount.Create(3);

        // act
        var actual = (int) playersCount;
        const int expected = 3;

        // assert
        Assert.Equal(expected, actual);
    }
}