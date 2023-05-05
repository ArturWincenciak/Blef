
using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class CheckingPlayerTests
{
    [Fact]
    public void CreateCheckingPlayerTest()
    {
        // arrange
        var guid = Guid.Parse("92F72527-350D-4C0B-BF64-CF65D65117A5");

        // act
        var actual = new CheckingPlayer(new(guid));

        // assert
        Assert.Equal(guid, actual.Player.Id);
        Assert.True(new CheckingPlayer(new(guid)) == actual);
    }

    [Fact]
    public void CannotCreateCheckingPlayerWithNullPlayerIdTest() =>
        Assert.Throws<ArgumentNullException>(() => new CheckingPlayer(null!));
}