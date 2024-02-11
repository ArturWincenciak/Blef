using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class FullHouseIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public FullHouseIsOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Diamonds),
                    new Card(FaceCard.Ace, Suit.Hearts),
                    new Card(FaceCard.Ace, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.King, Suit.Spades),
                    new Card(FaceCard.King, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenFullHouse(FaceCard.Ace, FaceCard.King));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Diamonds),
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Queen, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenFullHouse(FaceCard.Queen, FaceCard.Nine));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Diamonds),
                    new Card(FaceCard.Ten, Suit.Hearts),
                    new Card(FaceCard.Jack, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Spades),
                    new Card(FaceCard.Ace, Suit.Hearts)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Diamonds),
                    new Card(FaceCard.Ten, Suit.Clubs),
                    new Card(FaceCard.Nine, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Jack, Suit.Spades),
                    new Card(FaceCard.Jack, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenFullHouse(FaceCard.Nine, FaceCard.Jack));

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenFullHouse(FaceCard.Ten, FaceCard.Jack));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenFullHouse(FaceCard.King, FaceCard.Queen));
    }
}