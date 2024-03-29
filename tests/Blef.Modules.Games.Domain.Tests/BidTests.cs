﻿using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class BidTests
{
    [Fact]
    public void CreateBidTest()
    {
        // arrange
        var pokerHand = HighStraight.Create();
        var guid = Guid.Parse("C184A4D4-596C-4FBA-B41E-75C24AAF28CD");
        var playerId = new PlayerId(guid);

        // act
        var actual = new Bid(pokerHand, playerId);

        // assert
        Assert.Equal(pokerHand, actual.PokerHand);
        Assert.True(new PlayerId(guid) == actual.Player);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new Bid(PokerHand: null!, Player: null!));

    [Fact]
    public void CannotCreateWithNullPokerHandArgumentTest()
    {
        // arrange
        var guid = Guid.Parse("C184A4D4-596C-4FBA-B41E-75C24AAF28CD");
        var playerId = new PlayerId(guid);

        // act, assert
        Assert.Throws<ArgumentNullException>(() =>
            new Bid(PokerHand: null!, playerId));
    }

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest()
    {
        // arrange
        var pokerHand = HighStraight.Create();

        // act, assert
        Assert.Throws<ArgumentNullException>(() =>
            new Bid(pokerHand, Player: null!));
    }
}