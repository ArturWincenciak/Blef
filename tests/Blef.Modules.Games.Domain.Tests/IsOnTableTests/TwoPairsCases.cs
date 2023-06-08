using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests;

public static class TwoPairsCases
{
    public static IEnumerable<object[]> TableWithPokerHand =>
        new List<object[]>
        {
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Spades),
                        new Card(FaceCard.Ten, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Clubs),
                        new Card(FaceCard.Ace, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenTwoPairsBid(FaceCard.Nine, FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Diamonds),
                        new Card(FaceCard.Ten, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Clubs),
                        new Card(FaceCard.King, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Nine, Suit.Spades)
                    })
                }),
                PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ten)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[] {new Card(FaceCard.Queen, Suit.Diamonds)}),
                    new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                    new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                    new(new[] {new Card(FaceCard.Queen, Suit.Hearts)})
                }),
                PokerHandFactory.GivenTwoPairsBid(FaceCard.Jack, FaceCard.Queen)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Diamonds),
                        new Card(FaceCard.King, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Clubs),
                        new Card(FaceCard.King, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ten)
            }
        };
}
