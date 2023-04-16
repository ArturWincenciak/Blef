using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;
using Blef.Modules.Games.Domain.ValueObjects.PokerHands;

namespace Blef.Modules.Games.Domain.Tests;

public class BidTests
{
    [Fact]
    public void CreateBidTest()
    {
        var exception = Record.Exception(() =>
        {
            var pokerHand = HighStraight.Create();
            var guid = Guid.Parse("C184A4D4-596C-4FBA-B41E-75C24AAF28CD");
            var playerId = new PlayerId(guid);
            return new Bid(pokerHand, playerId);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new Bid(null, null));

    [Fact]
    public void CannotCreateWithNullPokerHandArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var guid = Guid.Parse("C184A4D4-596C-4FBA-B41E-75C24AAF28CD");
            var playerId = new PlayerId(guid);
            return new Bid(null, playerId);
        });

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var pokerHand = HighStraight.Create();
            return new Bid(pokerHand, null);
        });
}