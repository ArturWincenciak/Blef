using Blef.Modules.Games.Domain.Entities;
using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

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
        var playerGuid_1 = Guid.Parse("A034FDB8-C4CF-479E-B376-49C595560F9F");
        var playerGuid_2 = Guid.Parse("05E901E3-E4BD-4904-852A-C31B15A49F88");
        var playerHand_1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var playerHand_2 = new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)});
        var players = new DealPlayer[]
        {
            new(new(playerGuid_1), playerHand_1),
            new(new(playerGuid_2), playerHand_2)
        };
        var moveSequence = new MoveSequence(new []
        {
            new Move(new PlayerId(playerGuid_1), Order.Create(1)),
            new Move(new PlayerId(playerGuid_2), Order.Create(2)),
        });

        // act
        var actual = new Deal(dealId, new(new(players), moveSequence));

        // assert
        Assert.Equal(new DealId(new GameId(gameGuid), new DealNumber(1)), actual.Id);
    }
}