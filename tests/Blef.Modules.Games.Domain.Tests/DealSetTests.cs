using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealSetTests
{
    [Fact]
    public void CreateDealSetTest()
    {
        // arrange
        var playerId1 = new PlayerId(Guid.Parse("770FE75C-4F0D-43AF-8E11-801E07D3FA80"));
        var playerId2 = new PlayerId(Guid.Parse("FCF8192C-224E-4E53-8C5E-598C2BB4B273"));

        // act
        var exception = Record.Exception(() =>
        {
            return new DealSet(
                playersSet: new DealPlayersSet(new[]
                {
                    new DealPlayer(playerId1, Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
                    new DealPlayer(playerId2, Hand: new Hand(new Card[] {new(FaceCard.King, Suit.Clubs)}))
                }),
                moveSequence: new MoveSequence(new[]
                {
                    new Move(playerId1, Order.First),
                    new Move(playerId2, Order.First.Next)
                }));
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateDealSetWithNullPlayersSetTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealSet(playersSet: null!,
            moveSequence: new MoveSequence(new Move[]
            {
                new(Player: new PlayerId(Guid.Parse("1DC23566-3698-4621-93E0-E638C97A3472")), Order.First),
                new(Player: new PlayerId(Guid.Parse("4E550BC5-4ACC-4C09-A008-38243D6A5A05")), Order.First.Next)
            })));

    [Fact]
    public void CannotCreateDealSetWithNullMoveSequenceTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealSet(playersSet: new DealPlayersSet(new[]
        {
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("1DC23566-3698-4621-93E0-E638C97A3472")),
                Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("4E550BC5-4ACC-4C09-A008-38243D6A5A05")),
                Hand: new Hand(new Card[] {new(FaceCard.King, Suit.Clubs)}))
        }), moveSequence: null!));

    [Fact]
    public void CannotCreateDealSetWithNotAllPlayersPresentInMoveSequenceTest() =>
        Assert.Throws<ArgumentException>(() =>
        {
            // arrange
            var playerId1 = new PlayerId(Guid.Parse("770FE75C-4F0D-43AF-8E11-801E07D3FA80"));
            var playerId2 = new PlayerId(Guid.Parse("FCF8192C-224E-4E53-8C5E-598C2BB4B273"));
            var someOtherPlayer = new PlayerId(Guid.Parse("50CB0824-4242-43FF-97F4-6478FA074F36"));

            // act
            return new DealSet(
                playersSet: new DealPlayersSet(new[]
                {
                    new DealPlayer(playerId1, Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
                    new DealPlayer(playerId2, Hand: new Hand(new Card[] {new(FaceCard.King, Suit.Clubs)}))
                }),
                moveSequence: new MoveSequence(new[]
                {
                    new Move(playerId1, Order.First),
                    new Move(someOtherPlayer, Order.First.Next)
                }));
        });
}