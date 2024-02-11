using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class RoyalFlushIsNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public RoyalFlushIsNotOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Spades),
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.King, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Clubs)
                })
            }),
            p2: PokerHandFactory.GivenRoyalFlush(Suit.Spades));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Hearts),
                    new Card(FaceCard.Ten, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.King, Suit.Spades)
                })
            }),
            p2: PokerHandFactory.GivenRoyalFlush(Suit.Spades));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Diamonds),
                    new Card(FaceCard.Nine, Suit.Diamonds)
                })
            }),
            p2: PokerHandFactory.GivenRoyalFlush(Suit.Diamonds));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenRoyalFlush(Suit.Diamonds));
    }
}