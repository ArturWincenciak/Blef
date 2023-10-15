using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Api.Controllers.Games.Validators;

namespace Blef.Modules.Games.Api.Tests.UnitTests;

public class TwoPairsUniqueCardsAttributeTests
{
    [Fact]
    public void Valid()
    {
        // arrange
        var target = new TwoPairsUniqueCardsAttribute();
        var payload = new TwoPairsBidPayload(FaceCard.Ace, FaceCard.King);

        // act
        var actual = target.IsValid(payload);

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void Null()
    {
        // arrange
        var target = new TwoPairsUniqueCardsAttribute();
        TwoPairsBidPayload? nullPayload = null;

        // act
        var actual = target.IsValid(nullPayload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void TwoFaceCardAreEqual()
    {
        // arrange
        var target = new TwoPairsUniqueCardsAttribute();
        var payload = new TwoPairsBidPayload(FaceCard.Ace, FaceCard.Ace);

        // act
        var actual = target.IsValid(payload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void NotTwoPairsBidPayload()
    {
        // arrange
        var target = new TwoPairsUniqueCardsAttribute();
        var notValidType = "not a two pairs bid payload";

        // act
        var actual = target.IsValid(notValidType);

        // assert
        Assert.False(actual);
    }
}