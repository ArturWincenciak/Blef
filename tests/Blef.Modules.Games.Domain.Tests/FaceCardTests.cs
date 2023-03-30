using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Tests;

public class FaceCardTests
{
    [Fact]
    public void ShouldCompareFaceCards()
    {
        Assert.True(FaceCard.Jack < FaceCard.Queen);
        Assert.True(FaceCard.Queen < FaceCard.Ace);
    }
}