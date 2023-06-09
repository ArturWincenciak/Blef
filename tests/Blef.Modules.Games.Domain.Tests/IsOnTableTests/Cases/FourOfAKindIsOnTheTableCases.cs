using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class FourOfAKindIsOnTheTableCases
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
                        new Card(FaceCard.Ten, Suit.Clubs),
                        new Card(FaceCard.Ten, Suit.Diamonds),
                        new Card(FaceCard.Ten, Suit.Hearts),
                        new Card(FaceCard.Ten, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenFourOfAKind(FaceCard.Ten)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds),
                        new Card(FaceCard.Ace, Suit.Hearts),
                        new Card(FaceCard.Queen, Suit.Clubs),
                        new Card(FaceCard.Queen, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenFourOfAKind(FaceCard.Queen)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenFourOfAKind(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GetLowestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenFourOfAKind(FaceCard.King)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenFourOfAKind(FaceCard.Ten)
            }
        };
}