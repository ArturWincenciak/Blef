using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class FlushIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public FlushIsOnTheTableCases()
    {
        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Ten, Suit.Spades),
                    new Card(FaceCard.Nine, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Clubs)
                })
            }),
            PokerHandFactory.GivenFlush(Suit.Spades));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Diamonds),
                    new Card(FaceCard.Ace, Suit.Diamonds)
                })
            }),
            PokerHandFactory.GivenFlush(Suit.Diamonds));

        Add(TableCases.GetHighestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenFlush(Suit.Clubs));

        Add(TableCases.GetLowestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenFlush(Suit.Hearts));
    }
}