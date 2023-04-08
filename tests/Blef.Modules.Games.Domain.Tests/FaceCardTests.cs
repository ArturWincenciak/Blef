using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Tests;

public class FaceCardTests
{
    [Fact]
    public void RankTest()
    {
        Assert.True(FaceCard.King.GetRank() < FaceCard.Ace.GetRank());
        Assert.True(FaceCard.Queen.GetRank() < FaceCard.King.GetRank());
        Assert.True(FaceCard.Jack.GetRank() < FaceCard.Queen.GetRank());
        Assert.True(FaceCard.Ten.GetRank() < FaceCard.Jack.GetRank());
        Assert.True(FaceCard.Nine.GetRank() < FaceCard.Ten.GetRank());
    }

    [Fact]
    public void CompareTest()
    {
        Assert.True(FaceCard.King < FaceCard.Ace);
        Assert.True(FaceCard.Queen < FaceCard.King);
        Assert.True(FaceCard.Jack < FaceCard.Queen);
        Assert.True(FaceCard.Ten < FaceCard.Jack);
        Assert.True(FaceCard.Nine < FaceCard.Ten);

        Assert.True(FaceCard.Ace > FaceCard.King);
        Assert.True(FaceCard.King > FaceCard.Queen);
        Assert.True(FaceCard.Queen > FaceCard.Jack);
        Assert.True(FaceCard.Jack > FaceCard.Ten);
        Assert.True(FaceCard.Ten > FaceCard.Nine);

        Assert.True(FaceCard.Ace == FaceCard.Ace);
        Assert.True(FaceCard.Ace != FaceCard.King);
        Assert.True(FaceCard.King != FaceCard.Ace);
        Assert.True(FaceCard.King < FaceCard.Ace);
        Assert.True(FaceCard.Ace > FaceCard.King);

        Assert.True(FaceCard.Nine == FaceCard.Nine);
        Assert.True(FaceCard.Nine != FaceCard.Ten);
        Assert.True(FaceCard.Ten != FaceCard.Nine);
        Assert.True(FaceCard.Nine < FaceCard.Ten);
        Assert.True(FaceCard.Ten > FaceCard.Nine);

        Assert.True(FaceCard.Jack == FaceCard.Jack);
        Assert.True(FaceCard.Jack != FaceCard.Queen);
        Assert.True(FaceCard.Queen != FaceCard.Jack);
        Assert.True(FaceCard.Jack < FaceCard.Queen);
        Assert.True(FaceCard.Queen > FaceCard.Jack);

        Assert.True(FaceCard.King == FaceCard.King);
        Assert.True(FaceCard.Jack != FaceCard.King);
        Assert.True(FaceCard.King != FaceCard.Jack);
        Assert.True(FaceCard.Jack < FaceCard.King);
        Assert.True(FaceCard.King > FaceCard.Jack);
    }
}