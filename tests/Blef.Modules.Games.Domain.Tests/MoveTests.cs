using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class MoveTests
{
    [Fact]
    public void CreateMoveTest()
    {
        // arrange
        var player = new PlayerId(Guid.Parse("89FAAC08-D5A9-4611-98D4-EBDEA0749E67"));
        var order = Order.Create(sequence: 1);

        // act
        var exception = Record.Exception(() => new Move(player, order));

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateMoveWithNullPlayerTest() =>
        Assert.Throws<ArgumentNullException>(() => new Move(Player: null!, Order.First));

    [Fact]
    public void CannotCreateMoveWithNullOrderTest() =>
        Assert.Throws<ArgumentNullException>(() => new Move(
            Player: new(Guid.Parse("A2C87FD6-C1C4-4B6D-9CBD-CE41265050EA")),
            Order: null!));
}