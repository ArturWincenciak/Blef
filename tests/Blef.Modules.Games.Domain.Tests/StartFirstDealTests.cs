using Blef.Modules.Games.Domain.Events;
using Blef.Modules.Games.Domain.Tests.Mocks;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using static Blef.Modules.Games.Domain.Tests.Extensions.GameFactory;

namespace Blef.Modules.Games.Domain.Tests;

public class StartFirstDealTests
{
    [Fact]
    public void StartFirstDealTest()
    {
        // arrange
        var game = GivenGame();
        game.Join(new("Graham"));
        game.Join(new("Knuth"));

        // act
        var dealStarted = game.StartFirstDeal();

        // assert
        Assert.Equal(game.Id.Id, dealStarted.GameId);
        Assert.Equal(1, dealStarted.DealNumber);
        Assert.Equal(2, dealStarted.Players.Count());
        var actualGrahamPlayer = dealStarted.Players.First();
        var actualKnuthPlayer = dealStarted.Players.Last();
        var expectedGrahamCard = DeckFactoryMock.Cards[0];
        var expectedKnuthCard = DeckFactoryMock.Cards[1];
        Assert.NotEqual(Guid.Empty, actualGrahamPlayer.PlayerId);
        Assert.NotEqual(Guid.Empty, actualKnuthPlayer.PlayerId);
        Assert.True(actualKnuthPlayer.PlayerId != actualGrahamPlayer.PlayerId);
        Assert.Single(actualGrahamPlayer.Hand);
        Assert.Single(actualKnuthPlayer.Hand);
        AssertCard(expectedGrahamCard, actualGrahamPlayer.Hand.Single());
        AssertCard(expectedKnuthCard, actualKnuthPlayer.Hand.Single());
    }

    private static void AssertCard(Card expected, DealStarted.Card actual)
    {
        Assert.Equal(expected.FaceCard.ToString(), actual.FaceCard);
        Assert.Equal(expected.Suit.ToString(), actual.Suit);
    }
}