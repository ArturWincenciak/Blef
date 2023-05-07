using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealOrderPhysicTests
{
    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Zero_Then_GetOrder()
    {
        // arrange
        var playersCount = PlayersCount.Create(4);
        var dealsPlayedCount = DealsCount.Create(0);
        var orderPhysic = DealOrderPhysic.Create(playersCount, dealsPlayedCount);

        // act, assert
        Assert.Equal(Order.Create(1), orderPhysic.ShiftedOrder(Order.Create(1)));
        Assert.Equal(Order.Create(2), orderPhysic.ShiftedOrder(Order.Create(2)));
        Assert.Equal(Order.Create(3), orderPhysic.ShiftedOrder(Order.Create(3)));
        Assert.Equal(Order.Create(4), orderPhysic.ShiftedOrder(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_One_Then_GetOrder()
    {
        // arrange
        var playersCount = PlayersCount.Create(4);
        var dealsPlayedCount = DealsCount.Create(1);
        var orderPhysic = DealOrderPhysic.Create(playersCount, dealsPlayedCount);

        // act, assert
        Assert.Equal(Order.Create(4), orderPhysic.ShiftedOrder(Order.Create(1)));
        Assert.Equal(Order.Create(1), orderPhysic.ShiftedOrder(Order.Create(2)));
        Assert.Equal(Order.Create(2), orderPhysic.ShiftedOrder(Order.Create(3)));
        Assert.Equal(Order.Create(3), orderPhysic.ShiftedOrder(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Two_Then_GetOrder()
    {
        // arrange
        var playersCount = PlayersCount.Create(4);
        var dealsPlayedCount = DealsCount.Create(2);
        var orderPhysic = DealOrderPhysic.Create(playersCount, dealsPlayedCount);

        // act, assert
        Assert.Equal(Order.Create(3), orderPhysic.ShiftedOrder(Order.Create(1)));
        Assert.Equal(Order.Create(4), orderPhysic.ShiftedOrder(Order.Create(2)));
        Assert.Equal(Order.Create(1), orderPhysic.ShiftedOrder(Order.Create(3)));
        Assert.Equal(Order.Create(2), orderPhysic.ShiftedOrder(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Three_Then_GetOrder()
    {
        // arrange
        var playersCount = PlayersCount.Create(4);
        var dealsPlayedCount = DealsCount.Create(3);
        var orderPhysic = DealOrderPhysic.Create(playersCount, dealsPlayedCount);

        // act, assert
        Assert.Equal(Order.Create(2), orderPhysic.ShiftedOrder(Order.Create(1)));
        Assert.Equal(Order.Create(3), orderPhysic.ShiftedOrder(Order.Create(2)));
        Assert.Equal(Order.Create(4), orderPhysic.ShiftedOrder(Order.Create(3)));
        Assert.Equal(Order.Create(1), orderPhysic.ShiftedOrder(Order.Create(4)));
    }

    [Fact]
    public void Given_PlayerCount_Four_And_SequenceShift_Four_Then_GetOrder()
    {
        // arrange
        var playersCount = PlayersCount.Create(4);
        var dealsPlayedCount = DealsCount.Create(4);
        var orderPhysic = DealOrderPhysic.Create(playersCount, dealsPlayedCount);

        // act, assert
        Assert.Equal(Order.Create(1), orderPhysic.ShiftedOrder(Order.Create(1)));
        Assert.Equal(Order.Create(2), orderPhysic.ShiftedOrder(Order.Create(2)));
        Assert.Equal(Order.Create(3), orderPhysic.ShiftedOrder(Order.Create(3)));
        Assert.Equal(Order.Create(4), orderPhysic.ShiftedOrder(Order.Create(4)));
    }

    // todo: test with more deals count
    // todo: test with two players
    // todo: test with three players
}