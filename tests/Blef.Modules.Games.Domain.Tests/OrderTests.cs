using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

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

    [Fact]
    public void SecondPropertyEqualsCreatedSecondTest() =>
        Assert.True(Order.First.Next == Order.Create(2));

    [Fact]
    public void ThirdPropertyEqualsCreatedThirdTest() =>
        Assert.True(Order.First.Next.Next == Order.Create(3));

    [Fact]
    public void FourthPropertyEqualsCreatedFourthTest() =>
        Assert.True(Order.First.Next.Next.Next == Order.Create(4));

    [Fact]
    public void CannotCreateOrderWithSequenceLessThanOneTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Order.Create(sequence: 0));

    [Fact]
    public void CannotCreateOrderWithSequenceGreaterThanFourTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Order.Create(sequence: 5));

    [Fact]
    public void CannotCreateNextOrderOnFourthOrderTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => Order.First.Next.Next.Next.Next);
}