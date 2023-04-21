using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;

namespace Blef.Modules.Games.Domain.Tests;

public class GamePlayerTests
{
    [Fact]
    public void CreateGamePlayerTest()
    {
        // arrange
        var playerNick = new PlayerNick("Graham");

        // act
        var actual = GamePlayer.Create(playerNick);

        // assert
        Assert.Equal(new PlayerNick("Graham"), actual.Nick);
        Assert.True(CardsAmount.Initial == actual.CardsAmount);
        Assert.True(actual.IsInTheGame);
        Assert.True(actual.PlayerId.Id != Guid.Empty);
    }

    [Fact]
    public void When_LostOneTime_Then_PlayerIsInTheGameWithTwoCards()
    {
        // arrange
        var playerNick = new PlayerNick("Graham");
        var gamePlayer = GamePlayer.Create(playerNick);

        // act
        gamePlayer.LostLastDeal();
        var expected = CardsAmount.Initial.AddOneCard();

        // assert
        Assert.True(expected == gamePlayer.CardsAmount);
        Assert.True(gamePlayer.IsInTheGame);
    }

    [Fact]
    public void When_LostFourTimes_Then_PlayerIsInTheGameWithFiveCards()
    {
        // arrange
        var playerNick = new PlayerNick("Graham");
        var gamePlayer = GamePlayer.Create(playerNick);

        // act
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();

        var expected = CardsAmount.Initial
            .AddOneCard()
            .AddOneCard()
            .AddOneCard()
            .AddOneCard();

        // assert
        Assert.True(expected == gamePlayer.CardsAmount);
        Assert.True(gamePlayer.IsInTheGame);
    }

    [Fact]
    public void When_LostFiveTimes_Then_PlayerIsNotInTheGameWithFiveCards()
    {
        // arrange
        var playerNick = new PlayerNick("Graham");
        var gamePlayer = GamePlayer.Create(playerNick);

        // act
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();
        gamePlayer.LostLastDeal();

        var expected = CardsAmount.Initial
            .AddOneCard()
            .AddOneCard()
            .AddOneCard()
            .AddOneCard();

        // assert
        Assert.True(expected == gamePlayer.CardsAmount);
        Assert.False(gamePlayer.IsInTheGame);
    }

    [Fact]
    public void PlayerCannotLostMoreThenFiveTimesTest() =>
        Assert.Throws<InvalidOperationException>(() =>
        {
            // arrange
            var playerNick = new PlayerNick("Graham");
            var gamePlayer = GamePlayer.Create(playerNick);

            // act
            gamePlayer.LostLastDeal();
            gamePlayer.LostLastDeal();
            gamePlayer.LostLastDeal();
            gamePlayer.LostLastDeal();
            gamePlayer.LostLastDeal();
            gamePlayer.LostLastDeal();
        });
}