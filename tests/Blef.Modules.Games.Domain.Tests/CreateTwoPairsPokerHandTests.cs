using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class CreateTwoPairsPokerHandTests
{
    [Fact]
    public void ShouldThrowExceptionWhenFaceCardsAreTheSame()
    {
        // arrange
        var firstFaceCard = FaceCard.Ace;
        var secondFaceCard = FaceCard.Ace;

        // act, assert
        Assert.Throws<ArgumentException>(() =>
            TwoPairs.Create($"{firstFaceCard},{secondFaceCard}"));
    }
}