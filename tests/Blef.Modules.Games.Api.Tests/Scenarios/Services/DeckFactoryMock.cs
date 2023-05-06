using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Services;

namespace Blef.Modules.Games.Api.Tests.Scenarios.Services;

internal sealed class DeckFactoryMock : IDeckFactory
{
    public Deck Create()
    {
        var cards = new []
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

        return new Deck(cards);
    }
}