using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class CreateDealTests
{
    [Fact]
    public void CreateDealTest()
    {
        // arrange
        var dealNumber = new DealNumber(1);
        var gameGuid = Guid.Parse("9A7A3E9F-823C-4496-8356-08745BDE7EF6");
        var gameId = new GameId(gameGuid);
        var dealId = new DealId(gameId, dealNumber);
        var playerGuid1 = Guid.Parse("A034FDB8-C4CF-479E-B376-49C595560F9F");
        var playerGuid2 = Guid.Parse("05E901E3-E4BD-4904-852A-C31B15A49F88");
        var playerHand1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        var players = new DealPlayer[]
        {
            new(Player: new(playerGuid1), playerHand1),
            new(Player: new(playerGuid2), playerHand2)
        };
        var moveSequence = new MoveSequence(new[]
        {
            new Move(Player: new(playerGuid1), Order: Order.Create(1)),
            new Move(Player: new(playerGuid2), Order: Order.Create(2))
        });

        // act
        var actual = new Deal(dealId, dealSet: new(playersSet: new(players), moveSequence));

        // assert
        Assert.Equal(expected: new(Game: new(gameGuid), Deal: new(1)), actual.Id);
    }
}