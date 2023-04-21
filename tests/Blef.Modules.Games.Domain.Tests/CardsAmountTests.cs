using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class CardsAmountTests
{
    [Fact]
    public void InitialCardContainsOneCardTest()
    {
        // act
        var cardAmount = CardsAmount.Initial;

        // assert
        Assert.Equal(expected: 1, actual: (int) cardAmount);
    }

    [Fact]
    public void CanAddSeveralCardsTest()
    {
        // act
        var cardAmount = CardsAmount
            .Initial
            .AddOneCard()
            .AddOneCard()
            .AddOneCard();

        // assert
        Assert.Equal(expected: 4, actual: (int) cardAmount);
    }

    [Fact]
    public void CannotExceedFiveCardsTest() =>
        Assert.Throws<InvalidOperationException>(() => CardsAmount
            .Initial
            .AddOneCard()
            .AddOneCard()
            .AddOneCard()
            .AddOneCard()
            .AddOneCard());

    [Fact]
    public void InitialCardAmountIsLowerThenMaxCardAmountTest()
    {
        // act
        var actual = CardsAmount.Initial < CardsAmount.Max;

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void FourIsLowerThenMaxTest()
    {
        // arrange
        var cardsAmount = CardsAmount
            .Initial
            .AddOneCard()
            .AddOneCard()
            .AddOneCard();

        // act
        var actual = cardsAmount < CardsAmount.Max;

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void FiveIsNotLowerThenMaxTest()
    {
        // arrange
        var cardAmount = CardsAmount.Initial
            .AddOneCard()
            .AddOneCard()
            .AddOneCard()
            .AddOneCard();

        // act
        var actual = cardAmount < CardsAmount.Max;

        // assert
        Assert.False(actual);
    }
}