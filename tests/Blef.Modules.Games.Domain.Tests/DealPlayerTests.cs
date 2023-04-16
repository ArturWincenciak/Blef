using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class DealPlayerTests
{
    [Fact]
    public void CreateDealPlayerTest()
    {
        var exception = Record.Exception(() =>
        {
            // arrange
            var guid = Guid.Parse("9D51E124-53F0-49A5-B1F8-F010AFB6F110");
            var playerId = new PlayerId(guid);
            var hand = new Hand(new[] {new Card(FaceCard.Ace, Suit.Clubs)});

            // act
            var actual = new DealPlayer(playerId, hand);

            // assert
            Assert.Equal(expected: playerId, actual: actual.PlayerId);
            Assert.Equal(expected: hand, actual: actual.Hand);
        });
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealPlayer(null, null));

    [Fact]
    public void CannotCreateWithNullHandArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var guid = Guid.Parse("BED79350-F2E8-4C5B-8B5C-E62C9B01E9D2");
            var playerId = new PlayerId(guid);
            return new DealPlayer(playerId, null);
        });

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() =>
        {
            var hand = new Hand(new[] {new Card(FaceCard.Ace, Suit.Clubs)});
            return new DealPlayer(null, hand);
        });
}