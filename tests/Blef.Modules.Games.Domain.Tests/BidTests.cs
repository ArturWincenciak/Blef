using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class BidTests
{
    [Fact]
    public void CreateBidTest()
    {
        // arrange
        var pokerHand = HighStraight.Create();
        var guid = Guid.Parse("C184A4D4-596C-4FBA-B41E-75C24AAF28CD");
        var playerId = new PlayerId(guid);

        // act
        var actual = new Bid(pokerHand, playerId);

        // assert
        Assert.Equal(pokerHand, actual.PokerHand);
        Assert.Equal(playerId, actual.Player);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new Bid(PokerHand: null, Player: null));

    [Fact]
    public void CannotCreateWithNullPokerHandArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var guid = Guid.Parse("C184A4D4-596C-4FBA-B41E-75C24AAF28CD");
            var playerId = new PlayerId(guid);
            return new Bid(PokerHand: null, playerId);
        });

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var pokerHand = HighStraight.Create();
            return new Bid(pokerHand, Player: null);
        });
}