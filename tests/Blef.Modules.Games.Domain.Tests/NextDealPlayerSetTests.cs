using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Tests;

public class NextDealPlayerSetTests
{
    [Fact]
    public void CanCreateMinimalNextDealPlayerSetTest()
    {
        // arrange
        var playerId_1 = Guid.Parse("DBD6C198-DF42-4860-9984-403AF116335D");
        var playerId_2 = Guid.Parse("1AA646E3-8B90-43B8-8F39-094AA5A7AE77");
        var players = new NextDealPlayer[]
        {
            new (new (playerId_1), CardsAmount.Initial.AddOneCard(), 1),
            new (new (playerId_2), CardsAmount.Initial, 1)
        };

        // act
        var actual = new NextDealPlayersSet(players);

        // assert
        var player_1 = actual.Players.Single(player => player.Player == new PlayerId(playerId_1));
        Assert.True(player_1.CardsAmount == CardsAmount.Initial.AddOneCard());
        var player_2 = actual.Players.Single(player => player.Player == new PlayerId(playerId_2));
        Assert.True(player_2.CardsAmount == CardsAmount.Initial);
    }

    [Fact]
    public void CanCreateMaximalNextDealPlayerSetTest() =>
        Assert.Null(Record.Exception(() =>
            new NextDealPlayersSet(new NextDealPlayer[]
            {
                new(new(Guid.Parse("2EC70C1E-D3F9-459A-9FED-CD584A09FACA")), CardsAmount.Initial, 1),
                new(new(Guid.Parse("C23BC139-451E-4A69-AEF1-DA1A71D5423F")), CardsAmount.Initial, 2),
                new(new(Guid.Parse("882710F3-15CF-4BFB-9229-1FD8757A735B")), CardsAmount.Initial, 3),
                new(new(Guid.Parse("D649C278-6828-4924-AB66-651CAD931466")), CardsAmount.Initial, 4),
            })));

    [Fact]
    public void CannotCreateWithNullArgumentTest() =>
        Assert.Throws<ArgumentNullException>(() => new NextDealPlayersSet(null));

    [Fact]
    public void CannotCreateWithLessThenTwoPlayersTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new NextDealPlayersSet(new NextDealPlayer[]
            {
                new(new(Guid.Parse("658ABA3B-2B5F-4ABF-8E9C-9E802D1D81E0")), CardsAmount.Initial, 1)
            }));

    [Fact]
    public void CannotCreateWithMoreThenFourPlayersTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            new NextDealPlayersSet(new NextDealPlayer[]
            {
                new(new(Guid.Parse("D607A9B5-5E58-47FF-9A6E-2747DED5568E")), CardsAmount.Initial, 1),
                new(new(Guid.Parse("D05BE384-DA01-4337-9AEC-B126005B782B")), CardsAmount.Initial, 2),
                new(new(Guid.Parse("2074E8C3-0973-4676-A614-D26E091AC194")), CardsAmount.Initial, 3),
                new(new(Guid.Parse("81997B2C-19B7-413C-A3A5-B36831C70DA4")), CardsAmount.Initial, 4),
                new(new(Guid.Parse("5A875AD3-47AE-4659-9F5C-1B2EEDB3CE2D")), CardsAmount.Initial, 5),
            }));

    [Fact]
    public void CannotCreateWithNotAllUniquePlayers() =>
        Assert.Throws<ArgumentException>(() =>
        {
            var playerId = Guid.Parse("A15451BF-6BE2-4F26-A5DB-AC50FDAF1F77");
            return new NextDealPlayersSet(new NextDealPlayer[]
            {
                new(new(playerId), CardsAmount.Initial, 1),
                new(new(playerId), CardsAmount.Initial, 2),
            });
        });
}