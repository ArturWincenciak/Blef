using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class FullHouseIsOnTheTableCases
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
                        new Card(FaceCard.Ace, Suit.Diamonds),
                        new Card(FaceCard.Ace, Suit.Hearts),
                        new Card(FaceCard.Ace, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Spades),
                        new Card(FaceCard.King, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenFullHouse(FaceCard.Ace, FaceCard.King)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds),
                        new Card(FaceCard.Nine, Suit.Spades),
                        new Card(FaceCard.Queen, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenFullHouse(FaceCard.Queen, FaceCard.Nine)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Diamonds),
                        new Card(FaceCard.Ten, Suit.Hearts),
                        new Card(FaceCard.Jack, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Spades),
                        new Card(FaceCard.Ace, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Diamonds),
                        new Card(FaceCard.Ten, Suit.Clubs),
                        new Card(FaceCard.Nine, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Spades),
                        new Card(FaceCard.Jack, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenFullHouse(FaceCard.Nine, FaceCard.Jack)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenFullHouse(FaceCard.Ten, FaceCard.Jack)
            },
            new object[]
            {
                TableCases.GetLowestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenFullHouse(FaceCard.King, FaceCard.Queen)
            }
        };
}