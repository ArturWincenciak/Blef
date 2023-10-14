using Blef.Modules.Games.Domain.Model;

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
        Assert.True(new PlayerId(guid) == actual.Player);
        Assert.Equal(hand, actual.Hand);
    }

    [Fact]
    public void CannotCreateWithNullArgumentsTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealPlayer(Player: null!, Hand: null!));

    [Fact]
    public void CannotCreateWithNullHandArgumentTest()
    {
        // arrange
        var guid = Guid.Parse("BED79350-F2E8-4C5B-8B5C-E62C9B01E9D2");
        var playerId = new PlayerId(guid);

        // act, assert
        Assert.Throws<ArgumentNullException>(() =>
            new DealPlayer(playerId, Hand: null!));
    }

    [Fact]
    public void CannotCreateWithNullPlayerIdArgumentTest()
    {
        // arrange
        var hand = new Hand(new[] {new Card(FaceCard.Ace, Suit.Clubs)});

        // act, assert
        Assert.Throws<ArgumentNullException>(() =>
            new DealPlayer(Player: null!, hand));
    }
}