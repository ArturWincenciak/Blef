using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class StraightFlushIsNotOnTheTableCases
{
    public static IEnumerable<object[]> Cases =>
        new List<object[]>
        {
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Spades),
                        new Card(FaceCard.Ten, Suit.Spades),
                        new Card(FaceCard.Jack, Suit.Spades),
                        new Card(FaceCard.Queen, Suit.Spades),
                        new Card(FaceCard.King, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Clubs)
                    })
                }),
                PokerHandFactory.GivenStraightFlush(Suit.Spades)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Spades),
                        new Card(FaceCard.Ten, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Spades),
                        new Card(FaceCard.Queen, Suit.Spades),
                        new Card(FaceCard.Ace, Suit.Spades)
                    })
                }),
                PokerHandFactory.GivenStraightFlush(Suit.Spades)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds),
                        new Card(FaceCard.King, Suit.Diamonds)
                    })
                }),
                PokerHandFactory.GivenStraightFlush(Suit.Diamonds)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenStraightFlush(Suit.Hearts)
            }
        };
}
