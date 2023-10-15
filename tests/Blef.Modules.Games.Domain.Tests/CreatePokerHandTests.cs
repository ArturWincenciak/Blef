using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class CreatePokerHandTests
{
    [Fact]
    public void Given_FaceCardsAreTheSame_When_CreateTwoPairs_Then_ShouldThrowException()
    {
        // arrange
        var firstFaceCard = FaceCard.Ace;
        var secondFaceCard = FaceCard.Ace;

        // act, assert
        Assert.Throws<ArgumentException>(() =>
            TwoPairs.Create($"{firstFaceCard},{secondFaceCard}"));
    }

    [Fact]
    public void Given_FaceCardsAreTheSame_When_CreateFullHouse_Then_ShouldThrowException()
    {
        // arrange
        var threeOfAKind = FaceCard.Ace;
        var pair = FaceCard.Ace;

        // act, assert
        Assert.Throws<ArgumentException>(() =>
            FullHouse.Create($"{threeOfAKind},{pair}"));
    }
}