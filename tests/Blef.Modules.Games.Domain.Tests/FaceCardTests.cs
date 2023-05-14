using Blef.Modules.Games.Domain.Model;

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
        Assert.False(FaceCard.King.IsBetterThen(FaceCard.Ace));
        Assert.False(FaceCard.Queen.IsBetterThen(FaceCard.King));
        Assert.False(FaceCard.Jack.IsBetterThen(FaceCard.Queen));
        Assert.False(FaceCard.Ten.IsBetterThen(FaceCard.Jack));
        Assert.False(FaceCard.Nine.IsBetterThen(FaceCard.Ten));

        Assert.True(FaceCard.Ace.IsBetterThen(FaceCard.King));
        Assert.True(FaceCard.King.IsBetterThen(FaceCard.Queen));
        Assert.True(FaceCard.Queen.IsBetterThen(FaceCard.Jack));
        Assert.True(FaceCard.Jack.IsBetterThen(FaceCard.Ten));
        Assert.True(FaceCard.Ten.IsBetterThen(FaceCard.Nine));

        Assert.True(FaceCard.Ace.Equals(FaceCard.Ace));
        Assert.False(FaceCard.Ace.Equals(FaceCard.King));
        Assert.False(FaceCard.King.Equals(FaceCard.Ace));
        Assert.False(FaceCard.King.IsBetterThen(FaceCard.Ace));
        Assert.True(FaceCard.Ace.IsBetterThen(FaceCard.King));

        Assert.True(FaceCard.Nine.Equals(FaceCard.Nine));
        Assert.False(FaceCard.Nine.Equals(FaceCard.Ten));
        Assert.False(FaceCard.Ten.Equals(FaceCard.Nine));
        Assert.False(FaceCard.Nine.IsBetterThen(FaceCard.Ten));
        Assert.True(FaceCard.Ten.IsBetterThen(FaceCard.Nine));

        Assert.True(FaceCard.Jack.Equals(FaceCard.Jack));
        Assert.False(FaceCard.Jack.Equals(FaceCard.Queen));
        Assert.False(FaceCard.Queen.Equals(FaceCard.Jack));
        Assert.False(FaceCard.Jack.IsBetterThen(FaceCard.Queen));
        Assert.True(FaceCard.Queen.IsBetterThen(FaceCard.Jack));

        Assert.True(FaceCard.King.Equals(FaceCard.King));
        Assert.False(FaceCard.Jack.Equals(FaceCard.King));
        Assert.False(FaceCard.King.Equals(FaceCard.Jack));
        Assert.False(FaceCard.Jack.IsBetterThen(FaceCard.King));
        Assert.True(FaceCard.King.IsBetterThen(FaceCard.Jack));
    }
}