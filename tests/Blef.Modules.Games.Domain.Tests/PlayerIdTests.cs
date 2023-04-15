using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class PlayerIdTests
{
    [Fact]
    public void CreatePlayerIdTest()
    {
        var exception = Record.Exception(() => new PlayerId(Guid.NewGuid()));
        Assert.Null(exception);
    }

    [Fact]
    public void PlayerIdCannotContainEmptyGuid() =>
        Assert.Throws<ArgumentException>(() => new PlayerId(Guid.Empty));

    [Fact]
    public void TwoInstancesWithTheSameGuidAreEqualsTest()
    {
        // arrange
        var guid = Guid.NewGuid();
        var first = new PlayerId(guid);
        var second = new PlayerId(guid);

        // act
        var actual = first == second;

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void TwoInstancesWithDifferentGuidAreNotEqualTest()
    {
        // arrange
        var first = new PlayerId(Guid.NewGuid());
        var second = new PlayerId(Guid.NewGuid());

        // act
        var actual = first != second;

        // assert
        Assert.True(actual);
    }
}