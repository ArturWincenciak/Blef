﻿using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

public static class ThreeOfAKindIsNotOnTheTableCases
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
                        new Card(FaceCard.Queen, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Hearts)
                    })
                }),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Ten)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Diamonds)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.King, Suit.Hearts)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Queen, Suit.Clubs)
                    })
                }),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Queen)
            },
            new object[]
            {
                TableCases.GivenTable(new Hand[]
                {
                    new(new[]
                    {
                        new Card(FaceCard.Ace, Suit.Diamonds),
                        new Card(FaceCard.Ace, Suit.Hearts),
                        new Card(FaceCard.Nine, Suit.Clubs)
                    }),
                    new(new[]
                    {
                        new Card(FaceCard.Ten, Suit.Spades)
                    })
                }),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GetLowestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Ace)
            },
            new object[]
            {
                TableCases.GetHighestMaxCardsForFourPlayers(),
                PokerHandFactory.GivenThreeOfAKind(FaceCard.Nine)
            }
        };
}