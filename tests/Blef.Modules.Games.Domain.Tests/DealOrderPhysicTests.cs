using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealOrderPhysicTests
{
    [Fact]
    public void Test1()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);

        // act, assert
        Assert.Equal(1, orderPhysic.GetOrder(1));
        Assert.Equal(2, orderPhysic.GetOrder(2));
        Assert.Equal(3, orderPhysic.GetOrder(3));
        Assert.Equal(4, orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Test2()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(1);

        // act, assert
        Assert.Equal(4, orderPhysic.GetOrder(1));
        Assert.Equal(1, orderPhysic.GetOrder(2));
        Assert.Equal(2, orderPhysic.GetOrder(3));
        Assert.Equal(3, orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Test3()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(2);

        // act, assert
        Assert.Equal(3, orderPhysic.GetOrder(1));
        Assert.Equal(4, orderPhysic.GetOrder(2));
        Assert.Equal(1, orderPhysic.GetOrder(3));
        Assert.Equal(2, orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Test4()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(3);

        // act, assert
        Assert.Equal(2, orderPhysic.GetOrder(1));
        Assert.Equal(3, orderPhysic.GetOrder(2));
        Assert.Equal(4, orderPhysic.GetOrder(3));
        Assert.Equal(1, orderPhysic.GetOrder(4));
    }

    [Fact]
    public void Test5()
    {
        // arrange
        var orderPhysic = new DealOrderPhysic(4);
        orderPhysic.Move(4);

        // act, assert
        Assert.Equal(1, orderPhysic.GetOrder(1));
        Assert.Equal(2, orderPhysic.GetOrder(2));
        Assert.Equal(3, orderPhysic.GetOrder(3));
        Assert.Equal(4, orderPhysic.GetOrder(4));
    }
}