using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests;

public static class HighStraightCases
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
                TableCases.GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                PokerHandFactory.GivenHighCard(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                PokerHandFactory.GivenPair(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                PokerHandFactory.GivenTwoPairsBid(FaceCard.Ace, FaceCard.King)
            },
            new object[]
            {
                TableCases.GivenTopTableWithMaxPlayersWhoEachHaveMaxCards(),
                HighStraight.Create()
            },
            new object[]
            {
                TableCases.GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                PokerHandFactory.GivenHighCard(FaceCard.Nine)
            },
            new object[]
            {
                TableCases.GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                PokerHandFactory.GivenPair(FaceCard.Nine)
            },
            new object[]
            {
                TableCases.GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                PokerHandFactory.GivenTwoPairsBid(FaceCard.Nine, FaceCard.Ten)
            },
            new object[]
            {
                TableCases.GivenLowTableWithMaxPlayersWhoEachHaveMaxCards(),
                LowStraight.Create()
            }
        };
}