using Blef.Modules.Games.Domain.Model;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal static class TableCases
{
    internal static Table GivenTable(IReadOnlyCollection<Hand> hands) =>
        new(hands);

    internal static Table GetHighestMaxCardsForFourPlayers() =>
        GivenTable(new Hand[]
        {
            new(new[]
            {
                new Card(FaceCard.Ace, Suit.Spades),
                new Card(FaceCard.King, Suit.Spades),
                new Card(FaceCard.Queen, Suit.Spades),
                new Card(FaceCard.Jack, Suit.Spades),
                new Card(FaceCard.Ten, Suit.Spades)
            }),
            new(new[]
            {
                new Card(FaceCard.Ace, Suit.Diamonds),
                new Card(FaceCard.King, Suit.Diamonds),
                new Card(FaceCard.Queen, Suit.Diamonds),
                new Card(FaceCard.Jack, Suit.Diamonds),
                new Card(FaceCard.Ten, Suit.Diamonds)
            }),
            new(new[]
            {
                new Card(FaceCard.Ace, Suit.Clubs),
                new Card(FaceCard.King, Suit.Clubs),
                new Card(FaceCard.Queen, Suit.Clubs),
                new Card(FaceCard.Jack, Suit.Clubs),
                new Card(FaceCard.Ten, Suit.Clubs)
            }),
            new(new[]
            {
                new Card(FaceCard.Ace, Suit.Hearts),
                new Card(FaceCard.King, Suit.Hearts),
                new Card(FaceCard.Queen, Suit.Hearts),
                new Card(FaceCard.Jack, Suit.Hearts),
                new Card(FaceCard.Ten, Suit.Hearts)
            })
        });

    internal static Table GetLowestMaxCardsForFourPlayers() =>
        GivenTable(new Hand[]
        {
            new(new[]
            {
                new Card(FaceCard.King, Suit.Spades),
                new Card(FaceCard.Queen, Suit.Spades),
                new Card(FaceCard.Jack, Suit.Spades),
                new Card(FaceCard.Ten, Suit.Spades),
                new Card(FaceCard.Nine, Suit.Spades)
            }),
            new(new[]
            {
                new Card(FaceCard.King, Suit.Diamonds),
                new Card(FaceCard.Queen, Suit.Diamonds),
                new Card(FaceCard.Jack, Suit.Diamonds),
                new Card(FaceCard.Ten, Suit.Diamonds),
                new Card(FaceCard.Nine, Suit.Diamonds)
            }),
            new(new[]
            {
                new Card(FaceCard.King, Suit.Clubs),
                new Card(FaceCard.Queen, Suit.Clubs),
                new Card(FaceCard.Jack, Suit.Clubs),
                new Card(FaceCard.Ten, Suit.Clubs),
                new Card(FaceCard.Nine, Suit.Clubs)
            }),
            new(new[]
            {
                new Card(FaceCard.King, Suit.Hearts),
                new Card(FaceCard.Queen, Suit.Hearts),
                new Card(FaceCard.Jack, Suit.Hearts),
                new Card(FaceCard.Ten, Suit.Hearts),
                new Card(FaceCard.Nine, Suit.Hearts)
            })
        });
}