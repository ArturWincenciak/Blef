using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class HighStraightIsOnTheTableCases
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
                        new Card(FaceCard.Jack, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Spades)
                    })
                }),
                HighStraight.Create()
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Diamonds),
                        new Card(FaceCard.Nine, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Hearts),
                        new Card(FaceCard.Ace, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Spades)
                    })
                }),
                HighStraight.Create()
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Spades),
                        new Card(FaceCard.Jack, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Clubs),
                        new Card(FaceCard.Queen, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Spades),
                        new Card(FaceCard.Ace, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Spades)
                    })
                }),
                HighStraight.Create()
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Spades),
                        new Card(FaceCard.King, Suit.Hearts),
                        new Card(FaceCard.King, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Jack, Suit.Hearts),
                        new Card(FaceCard.King, Suit.Clubs),
                        new Card(FaceCard.Queen, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Spades),
                        new Card(FaceCard.Ace, Suit.Clubs),
                        new Card(FaceCard.Ace, Suit.Spades)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Hearts)
                    })
                }),
                HighStraight.Create()
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                HighStraight.Create()
            }
        };
}