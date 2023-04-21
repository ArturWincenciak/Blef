﻿using Blef.Modules.Games.Domain.Services;
using Blef.Modules.Games.Domain.ValueObjects.Cards;

namespace Blef.Modules.Games.Domain.Tests;

public class CroupierTests
{
    [Fact]
    public void CreateCroupierTest() =>
        Assert.Null(Record.Exception(() =>
        {
            var deckFactory = new DeckFactoryMock();
            return new Croupier(deckFactory);
        }));

    private class DeckFactoryMock : IDeckFactory
    {
        public Deck Create() =>
            new(Cards);

        private static Card[] Cards =>
            new[]
            {
                new Card(FaceCard.Ace, Suit.Diamonds),
                new Card(FaceCard.Ace, Suit.Spades),
                new Card(FaceCard.Ten, Suit.Clubs),
                new Card(FaceCard.Jack, Suit.Spades),
                new Card(FaceCard.Queen, Suit.Diamonds),
                new Card(FaceCard.King, Suit.Hearts),
                new Card(FaceCard.King, Suit.Clubs),
                new Card(FaceCard.Ace, Suit.Clubs),
                new Card(FaceCard.Queen, Suit.Clubs),
                new Card(FaceCard.Jack, Suit.Diamonds),
                new Card(FaceCard.Ten, Suit.Diamonds),
                new Card(FaceCard.King, Suit.Diamonds),
                new Card(FaceCard.Nine, Suit.Clubs),
                new Card(FaceCard.King, Suit.Spades),
                new Card(FaceCard.Queen, Suit.Spades),
                new Card(FaceCard.Jack, Suit.Clubs),
                new Card(FaceCard.Nine, Suit.Spades),
                new Card(FaceCard.Ace, Suit.Hearts),
                new Card(FaceCard.Ten, Suit.Spades),
                new Card(FaceCard.Ten, Suit.Hearts),
                new Card(FaceCard.Nine, Suit.Hearts),
                new Card(FaceCard.Nine, Suit.Diamonds),
                new Card(FaceCard.Jack, Suit.Hearts),
                new Card(FaceCard.Queen, Suit.Hearts)
            };
    }
}