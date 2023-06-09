using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class FlushIsOnTheTableCases
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
                        new Card(FaceCard.Queen, Suit.Spades),
                        new Card(FaceCard.Jack, Suit.Spades),
                        new Card(FaceCard.Ten, Suit.Spades),
                        new Card(FaceCard.Nine, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Clubs)
                    })
                }),
                PokerHandFactory.GivenFlush(Suit.Spades)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Diamonds),
                        new Card(FaceCard.Ace, Suit.Diamonds)
                    })
                }),
                PokerHandFactory.GivenFlush(Suit.Diamonds)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenFlush(Suit.Clubs)
            },
            new object[]
            {
                TableCases.GetLowestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenFlush(Suit.Hearts)
            }
        };
}