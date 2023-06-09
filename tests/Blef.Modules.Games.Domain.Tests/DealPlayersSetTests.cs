using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests;

public class DealPlayersSetTests
{
    [Fact]
    public void CreateDealPlayersSetTest()
    {
        // act
        var exception = Record.Exception(() =>
        {
            return new DealPlayersSet(new[]
            {
                new DealPlayer(
                    Player: new PlayerId(Guid.Parse("770FE75C-4F0D-43AF-8E11-801E07D3FA80")),
                    Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
                new DealPlayer(
                    Player: new PlayerId(Guid.Parse("E40189FE-9C6C-43FE-A2D9-82A1DF613A22")),
                    Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Diamonds)}))
            });
        });

        // assert
        Assert.Null(exception);
    }

    [Fact]
    public void CannotCreateDealPlayersSetWithNullPlayersTest() =>
        Assert.Throws<ArgumentNullException>(() => new DealPlayersSet(null!));

    [Fact]
    public void CannotCreateDealPlayersSetWithLessThanTwoPlayersTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new DealPlayersSet(new[]
        {
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("B84CAE38-A018-4ED1-B3DD-D5C808D4ED97")),
                Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)}))
        }));

    [Fact]
    public void CannotCreateDealPlayersSetWithMoreThanFourPlayersTest() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => new DealPlayersSet(new[]
        {
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("63CA4C50-09AC-4672-86B6-529112B597D1")),
                Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("7A8AAA59-47F6-4FA0-BD42-B91E82A3D364")),
                Hand: new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("430C1987-3F45-4A86-AEE5-0ECD74F10388")),
                Hand: new Hand(new Card[] {new(FaceCard.Queen, Suit.Diamonds)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("C54018C5-21AA-4DD9-BD19-0684132360F1")),
                Hand: new Hand(new Card[] {new(FaceCard.Jack, Suit.Diamonds)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("E40189FE-9C6C-43FE-A2D9-82A1DF613A25")),
                Hand: new Hand(new Card[] {new(FaceCard.Ten, Suit.Diamonds)}))
        }));

    [Fact]
    public void CannotCreateDealPlayersSetWithDuplicatePlayersTest() =>
        Assert.Throws<ArgumentException>(() => new DealPlayersSet(new[]
        {
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("63CA4C50-09AC-4672-86B6-529112B597D1")),
                Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("63CA4C50-09AC-4672-86B6-529112B597D1")),
                Hand: new Hand(new Card[] {new(FaceCard.King, Suit.Diamonds)}))
        }));

    [Fact]
    public void CannotCreateDealPlayersSetWithDuplicateCardsTest() =>
        Assert.Throws<ArgumentException>(() => new DealPlayersSet(new[]
        {
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("2F0EA654-37D0-4551-91DA-D807D46E2257")),
                Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)})),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("096708B9-E44F-4A30-904C-AFED90A9E292")),
                Hand: new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)}))
        }));

    [Fact]
    public void GetTableTest()
    {
        // arrange
        var hand1 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Clubs)});
        var hand2 = new Hand(new Card[] {new(FaceCard.Ace, Suit.Diamonds), new(FaceCard.King, Suit.Diamonds)});
        var dealPlayersSet = new DealPlayersSet(new[]
        {
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("B0B1F83F-B61F-45A6-9FCE-DD5285876BB3")), hand1),
            new DealPlayer(
                Player: new PlayerId(Guid.Parse("A6303BC3-1FE0-4AC1-A49E-9000227C3520")), hand2)
        });

        // act
        var actual = dealPlayersSet.Table();

        // assert
        Assert.True(actual.Contains(hand1.Cards.First().FaceCard));
        Assert.True(actual.Contains(hand2.Cards.First().FaceCard));
        Assert.True(actual.Contains(hand2.Cards.Last().FaceCard));
        Assert.Equal(expected: 2, actual: actual.Count(FaceCard.Ace));
        Assert.Equal(expected: 1, actual: actual.Count(FaceCard.King));
        Assert.Equal(expected: 1, actual: actual.Count(Suit.Clubs));
        Assert.Equal(expected: 2, actual: actual.Count(Suit.Diamonds));
    }
}
