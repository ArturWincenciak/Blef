using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class PlayerIdTests
{
    [Fact]
    public void CreatePlayerIdTest()
    {
        var guid = Guid.Parse("ED470554-C946-40D7-880C-D46BF99A88F7");
        var exception = Record.Exception(() => new PlayerId(guid));
        Assert.Null(exception);
    }

    [Fact]
    public void PlayerIdCannotContainEmptyGuid() =>
        Assert.Throws<ArgumentException>(() => new PlayerId(Guid.Empty));

    [Fact]
    public void TwoInstancesWithTheSameGuidAreEqualsTest()
    {
        // arrange
        var guid = Guid.Parse("FF98A20E-069B-4D46-94B9-63FD5C625D3C");
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
        var first = new PlayerId(Guid.Parse("5CCD73E7-F355-4BD6-A8D4-6538285BD5DE"));
        var second = new PlayerId(Guid.Parse("B954F622-9FB2-41B9-9F37-7A3D019DB15D"));

        // act
        var actual = first != second;

        // assert
        Assert.True(actual);
    }
}