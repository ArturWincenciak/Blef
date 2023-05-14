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
        Assert.True(FaceCard.Ace == actual.FaceCard);
        Assert.Equal(expected: Suit.Clubs, actual.Suit);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new Card(null!, null!));

    [Fact]
    public void CannotCreateWithNullFaceCardArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new Card(null!, Suit.Clubs));

    [Fact]
    public void CannotCreateWithNullSuitArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new Card(FaceCard.Ace, null!));
}