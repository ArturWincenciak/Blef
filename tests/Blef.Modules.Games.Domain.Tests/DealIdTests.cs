﻿using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealIdTests
{
    [Fact]
    public void CreateDealIdTest()
    {
        // arrange
        var guid = Guid.Parse("ED665A22-74CF-4466-80FF-9BC005889054");
        var gameId = new GameId(guid);
        var dealNumber = new DealNumber(1);

        // act
        var actual = new DealId(gameId, dealNumber);

        // assert
        Assert.True(new GameId(guid) == actual.Game);
        Assert.True(new DealNumber(1) == actual.Deal);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealId(Game: null!, Deal: null!));

    [Fact]
    public void CannotCreateWithNullGameIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealId(Game: null!, Deal: new(1)));

    [Fact]
    public void CannotCreateWithNullDealNumberArgumentTest()
    {
        // arrange
        var guid = Guid.Parse("458C41E2-C477-4918-9353-96243FEFF99F");
        var gameId = new GameId(guid);

        // act, assert
        Assert.Throws<ArgumentNullException>(() =>
            new DealId(gameId, Deal: null!));
    }
}