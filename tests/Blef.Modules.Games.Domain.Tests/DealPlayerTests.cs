using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealPlayerTests
{
    [Fact]
    public void CreateDealPlayerTest()
    {
        // arrange
        var guid = Guid.Parse("9D51E124-53F0-49A5-B1F8-F010AFB6F110");
        var playerId = new PlayerId(guid);
        var hand = new Hand(new[] {new Card(FaceCard.Ace, Suit.Clubs)});

        // act
        var actual = new DealPlayer(playerId, hand);

        // assert
        Assert.Equal(playerId, actual.PlayerId);
        Assert.Equal(hand, actual.Hand);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealPlayer(PlayerId: null, Hand: null));

    [Fact]
    public void CannotCreateWithNullHandArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var guid = Guid.Parse("BED79350-F2E8-4C5B-8B5C-E62C9B01E9D2");
            var playerId = new PlayerId(guid);
            return new DealPlayer(playerId, Hand: null);
        });

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var hand = new Hand(new[] {new Card(FaceCard.Ace, Suit.Clubs)});
            return new DealPlayer(PlayerId: null, hand);
        });
}