using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void CreateOrderTest()
    {
        // act
        var exception = Record.Exception(() =>
        {
            Order.Create(sequence: 1);
            Order.Create(sequence: 2);
            Order.Create(sequence: 3);
            Order.Create(sequence: 4);
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void FirstPropertyEqualsCreatedFirstTest() =>
        Assert.True(Order.First == Order.Create(1));
}