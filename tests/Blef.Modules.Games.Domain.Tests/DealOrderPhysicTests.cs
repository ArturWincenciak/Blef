using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealOrderPhysicTests
{
    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Zero_Then_GetOrder()
    {
        // arrange
        var orderPhysic = DealOrderPhysic.Create(4, 0);

        // act, assert
        Assert.Equal(Order.Create(1), orderPhysic.Order(Order.Create(1)));
        Assert.Equal(Order.Create(2), orderPhysic.Order(Order.Create(2)));
        Assert.Equal(Order.Create(3), orderPhysic.Order(Order.Create(3)));
        Assert.Equal(Order.Create(4), orderPhysic.Order(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_One_Then_GetOrder()
    {
        // arrange
        var orderPhysic = DealOrderPhysic.Create(4, 1);

        // act, assert
        Assert.Equal(Order.Create(4), orderPhysic.Order(Order.Create(1)));
        Assert.Equal(Order.Create(1), orderPhysic.Order(Order.Create(2)));
        Assert.Equal(Order.Create(2), orderPhysic.Order(Order.Create(3)));
        Assert.Equal(Order.Create(3), orderPhysic.Order(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Two_Then_GetOrder()
    {
        // arrange
        var orderPhysic = DealOrderPhysic.Create(4, 2);

        // act, assert
        Assert.Equal(Order.Create(3), orderPhysic.Order(Order.Create(1)));
        Assert.Equal(Order.Create(4), orderPhysic.Order(Order.Create(2)));
        Assert.Equal(Order.Create(1), orderPhysic.Order(Order.Create(3)));
        Assert.Equal(Order.Create(2), orderPhysic.Order(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Three_Then_GetOrder()
    {
        // arrange
        var orderPhysic = DealOrderPhysic.Create(4, 3);

        // act, assert
        Assert.Equal(Order.Create(2), orderPhysic.Order(Order.Create(1)));
        Assert.Equal(Order.Create(3), orderPhysic.Order(Order.Create(2)));
        Assert.Equal(Order.Create(4), orderPhysic.Order(Order.Create(3)));
        Assert.Equal(Order.Create(1), orderPhysic.Order(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Four_Then_GetOrder()
    {
        // arrange
        var orderPhysic = DealOrderPhysic.Create(4, 4);

        // act, assert
        Assert.Equal(Order.Create(1), orderPhysic.Order(Order.Create(1)));
        Assert.Equal(Order.Create(2), orderPhysic.Order(Order.Create(2)));
        Assert.Equal(Order.Create(3), orderPhysic.Order(Order.Create(3)));
        Assert.Equal(Order.Create(4), orderPhysic.Order(Order.Create(4)));
    }
}