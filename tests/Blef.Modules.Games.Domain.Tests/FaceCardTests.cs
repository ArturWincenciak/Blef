using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class FaceCardTests
{
    [Fact]
    public void CreateFaceCardTest()
    {
        // act
        var ace = FaceCard.Ace;
        var king = FaceCard.King;
        var queen = FaceCard.Queen;
        var jack = FaceCard.Jack;
        var ten = FaceCard.Ten;
        var nine = FaceCard.Nine;

        // assert
        Assert.Equal(ace, actual: FaceCard.Create("ace"));
        Assert.Equal(king, actual: FaceCard.Create("king"));
        Assert.Equal(queen, actual: FaceCard.Create("queen"));
        Assert.Equal(jack, actual: FaceCard.Create("jack"));
        Assert.Equal(ten, actual: FaceCard.Create("ten"));
        Assert.Equal(nine, actual: FaceCard.Create("nine"));
    }

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

        Assert.False(FaceCard.Ace.Equals(FaceCard.King));
        Assert.False(FaceCard.King.Equals(FaceCard.Ace));
        Assert.False(FaceCard.King.IsBetterThen(FaceCard.Ace));
        Assert.True(FaceCard.Ace.IsBetterThen(FaceCard.King));

        Assert.False(FaceCard.Nine.Equals(FaceCard.Ten));
        Assert.False(FaceCard.Ten.Equals(FaceCard.Nine));
        Assert.False(FaceCard.Nine.IsBetterThen(FaceCard.Ten));
        Assert.True(FaceCard.Ten.IsBetterThen(FaceCard.Nine));

        Assert.False(FaceCard.Jack.Equals(FaceCard.Queen));
        Assert.False(FaceCard.Queen.Equals(FaceCard.Jack));
        Assert.False(FaceCard.Jack.IsBetterThen(FaceCard.Queen));
        Assert.True(FaceCard.Queen.IsBetterThen(FaceCard.Jack));

        Assert.False(FaceCard.Jack.Equals(FaceCard.King));
        Assert.False(FaceCard.King.Equals(FaceCard.Jack));
        Assert.False(FaceCard.Jack.IsBetterThen(FaceCard.King));
        Assert.True(FaceCard.King.IsBetterThen(FaceCard.Jack));
    }
}