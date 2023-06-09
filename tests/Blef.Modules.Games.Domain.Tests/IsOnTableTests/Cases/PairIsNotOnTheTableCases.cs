using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class PairIsNotOnTheTableCases
{
    public static IEnumerable<object[]> Cases =>
        new List<object[]>
        {
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Nine, Suit.Spades)}),
                    new(new[] {new Card(FaceCard.Nine, Suit.Diamonds)})
                }),
                PokerHandFactory.GivenPair(FaceCard.Ten)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ten, Suit.Diamonds)}),
                    new(new[] {new Card(FaceCard.King, Suit.Clubs)}),
                    new(new[] {new Card(FaceCard.Ten, Suit.Spades)})
                }),
                PokerHandFactory.GivenPair(FaceCard.King)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                    new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                    new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                    new(new[] {new Card(FaceCard.Ace, Suit.Hearts)})
                }),
                PokerHandFactory.GivenPair(FaceCard.King)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Ace, Suit.Diamonds)}),
                    new(new[] {new Card(FaceCard.Ace, Suit.Spades)}),
                    new(new[] {new Card(FaceCard.Ace, Suit.Hearts)}),
                    new(new[] {new Card(FaceCard.Ace, Suit.Clubs)})
                }),
                PokerHandFactory.GivenPair(FaceCard.Nine)
            },
            new object[]
            {
                TableCases.GetLowestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenPair(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenPair(FaceCard.Nine)
            }
        };
}
