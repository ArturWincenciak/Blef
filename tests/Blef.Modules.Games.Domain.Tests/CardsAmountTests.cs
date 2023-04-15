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
        // arrange
        var cardAmount = CardsAmount.Initial;

        // act
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();

        // assert
        Assert.Equal(expected: 4, actual: (int) cardAmount);
    }

    [Fact]
    public void CannotExceedFiveCardsTest()
    {
        // arrange
        var cardAmount = CardsAmount.Initial;
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();

        // act, assert
        Assert.Throws<InvalidOperationException>(() => cardAmount.AddOneCard());
    }

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
        var cardAmount = CardsAmount.Initial;
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();

        // act
        var actual = cardAmount < CardsAmount.Max;

        // assert
        Assert.True(actual);
    }

    [Fact]
    public void FiveIsNotLowerThenMaxTest()
    {
        // arrange
        var cardAmount = CardsAmount.Initial;
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();
        cardAmount.AddOneCard();

        // act
        var actual = cardAmount < CardsAmount.Max;

        // assert
        Assert.False(actual);
    }
}