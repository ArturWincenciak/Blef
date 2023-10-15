using Blef.Modules.Games.Api.Controllers.Games.Commands;
using Blef.Modules.Games.Api.Controllers.Games.Validators;

namespace Blef.Modules.Games.Api.Tests.UnitTests;

public class FullHouseUniqueCardsAttributeTests
{
    [Fact]
    public void Valid()
    {
        // arrange
        var target = new FullHouseUniqueCardsAttribute();
        var payload = new FullHouseBidPayload(FaceCard.Ace, FaceCard.King);

        // act
        var actual = target.IsValid(payload);

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void Null()
    {
        // arrange
        var target = new FullHouseUniqueCardsAttribute();
        FullHouseBidPayload? nullPayload = null;

        // act
        var actual = target.IsValid(nullPayload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void ThreeOfAKindEqualsPair()
    {
        // arrange
        var target = new FullHouseUniqueCardsAttribute();
        var payload = new FullHouseBidPayload(FaceCard.Ace, FaceCard.Ace);

        // act
        var actual = target.IsValid(payload);

        // assert
        Assert.False(actual);
    }

    [Fact]
    public void NotFullHouseBidPayload()
    {
        // arrange
        var target = new FullHouseUniqueCardsAttribute();
        var notValidType = "not a full house bid payload";

        // act
        var actual = target.IsValid(notValidType);

        // assert
        Assert.False(actual);
    }
}