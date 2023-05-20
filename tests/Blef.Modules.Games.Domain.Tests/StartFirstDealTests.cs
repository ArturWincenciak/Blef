using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Mocks;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class StartFirstDealTests
{
    [Fact]
    public void StartFirstDealTest()
    {
        // arrange
        var game = GivenGame();
        game.Join(new PlayerNick("Graham"));
        game.Join(new PlayerNick("Knuth"));

        // act
        var dealStarted = game.StartFirstDeal();

        // assert
        Assert.Equal(game.Id, dealStarted.Game);
        Assert.Equal(expected: new DealNumber(1), dealStarted.Deal);
        Assert.Equal(expected: 2, dealStarted.Players.Count);
        var actualGrahamPlayer = dealStarted.Players.First();
        var actualKnuthPlayer = dealStarted.Players.Last();
        var expectedGrahamCard = DeckFactoryMock.Cards[0];
        var expectedKnuthCard = DeckFactoryMock.Cards[1];
        Assert.NotEqual(Guid.Empty, actualGrahamPlayer.Player.Id);
        Assert.NotEqual(Guid.Empty, actualKnuthPlayer.Player.Id);
        Assert.True(actualKnuthPlayer.Player.Id != actualGrahamPlayer.Player.Id);
        Assert.Single(actualGrahamPlayer.Hand.Cards);
        Assert.Single(actualKnuthPlayer.Hand.Cards);
        Assert.True(expectedGrahamCard == actualGrahamPlayer.Hand.Cards.Single());
        Assert.True(expectedKnuthCard == actualKnuthPlayer.Hand.Cards.Single());
    }
}