﻿using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class TwoPairsAreNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public TwoPairsAreNotOnTheTableCases()
    {
        Add(TableCases.GivenTable(new Hand[]
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
            PokerHandFactory.GivenTwoPairsBid(FaceCard.Nine, FaceCard.Jack));

        Add(TableCases.GivenTable(new Hand[]
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
            PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ace));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Queen, Suit.Diamonds)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Spades)}),
                new(new[] {new Card(FaceCard.Jack, Suit.Hearts)}),
                new(new[] {new Card(FaceCard.Queen, Suit.Hearts)})
            }),
            PokerHandFactory.GivenTwoPairsBid(FaceCard.Jack, FaceCard.King));

        Add(TableCases.GivenTable(new Hand[]
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
            PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ace));

        Add(TableCases.GetHighestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Nine));

        Add(TableCases.GetLowestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenTwoPairsBid(FaceCard.King, FaceCard.Ace));
    }
}