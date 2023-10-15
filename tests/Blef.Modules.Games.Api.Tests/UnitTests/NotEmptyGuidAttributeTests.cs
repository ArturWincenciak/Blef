using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Api.Controllers.Games.Validators;

namespace Blef.Modules.Games.Api.Tests.UnitTests;

public class NotEmptyGuidAttributeTests
{
    [Fact]
    public void Valid()
    {
        // arrange
        var target = new NotEmptyGuidAttribute();
        var correctGuid = Guid.Parse("1FF799FA-15ED-425C-8B74-C3F307993C84");

        // act
        var actual = target.IsValid(correctGuid);

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void NotGuid()
    {
        // arrange
        var target = new NotEmptyGuidAttribute();
        var notGuid = "not a guid";

        // act
        var actual = target.IsValid(notGuid);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void EmptyGuid()
    {
        // arrange
        var target = new NotEmptyGuidAttribute();
        var emptyGuid = Guid.Empty;

        // act
        var actual = target.IsValid(emptyGuid);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void NullGuid()
    {
        // arrange
        var target = new NotEmptyGuidAttribute();
        Guid? nullGuid = null;

        // act
        var actual = target.IsValid(nullGuid);

        // assert
        Assert.False(actual);
    }


    [Fact]
    public void CorrectBidsRoute()
    {
        // arrange
        var target = new NotEmptyGuidAttribute();
        BidsRoute bidsRoute = new FlushBidsRoute(
            GameId: Guid.Parse("4A3AB946-CF4E-4CFC-8B5E-B8294CD04854"),
            PlayerId: Guid.Parse("CC7FE85E-7328-471B-8014-E2AD0914097E"));

        // act
        var actualGameId = target.IsValid(bidsRoute.GameId);
        var actualPlayerId = target.IsValid(bidsRoute.PlayerId);

        // assert
        Assert.True(actualGameId);
        Assert.True(actualPlayerId);
    }
}