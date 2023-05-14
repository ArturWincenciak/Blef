using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class CardTests
{
    [Fact]
    public void CreateCardTest()
    {
        // act
        var actual = new Card(FaceCard.Ace, Suit.Clubs);

        // assert
        Assert.True(FaceCard.Ace.Equals(actual.FaceCard));
        Assert.Equal(Suit.Clubs, actual.Suit);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new Card(FaceCard: null!, Suit: null!));

    [Fact]
    public void CannotCreateWithNullFaceCardArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new Card(FaceCard: null!, Suit.Clubs));

    [Fact]
    public void CannotCreateWithNullSuitArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new Card(FaceCard.Ace, Suit: null!));
}