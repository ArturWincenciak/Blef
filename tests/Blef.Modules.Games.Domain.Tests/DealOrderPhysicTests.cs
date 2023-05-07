using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealOrderPhysicTests
{
    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Zero_Then_GetOrder()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);

        // act, assert
        Assert.Equal(Order.Create(1), orderPhysic.GetOrder(1));
        Assert.Equal(Order.Create(2), orderPhysic.GetOrder(2));
        Assert.Equal(Order.Create(3), orderPhysic.GetOrder(3));
        Assert.Equal(Order.Create(4), orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_One_Then_GetOrder()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(1);

        // act, assert
        Assert.Equal(Order.Create(4), orderPhysic.GetOrder(1));
        Assert.Equal(Order.Create(1), orderPhysic.GetOrder(2));
        Assert.Equal(Order.Create(2), orderPhysic.GetOrder(3));
        Assert.Equal(Order.Create(3), orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Two_Then_GetOrder()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(playersCount: 4);
        orderPhysic.Move(sequenceShift: 2);

        // act, assert
        Assert.Equal(Order.Create(3), orderPhysic.GetOrder(1));
        Assert.Equal(Order.Create(4), orderPhysic.GetOrder(2));
        Assert.Equal(Order.Create(1), orderPhysic.GetOrder(3));
        Assert.Equal(Order.Create(2), orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Three_Then_GetOrder()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(3);

        // act, assert
        Assert.Equal(Order.Create(2), orderPhysic.GetOrder(1));
        Assert.Equal(Order.Create(3), orderPhysic.GetOrder(2));
        Assert.Equal(Order.Create(4), orderPhysic.GetOrder(3));
        Assert.Equal(Order.Create(1), orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Four_Then_GetOrder()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(4);

        // act, assert
        Assert.Equal(Order.Create(1), orderPhysic.GetOrder(1));
        Assert.Equal(Order.Create(2), orderPhysic.GetOrder(2));
        Assert.Equal(Order.Create(3), orderPhysic.GetOrder(3));
        Assert.Equal(Order.Create(4), orderPhysic.GetOrder(4));
    }
}