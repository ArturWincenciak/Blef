using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class HighCardIsNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public HighCardIsNotOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                new(new[] {new Card(FaceCard.King, Suit.Diamonds)})
            }),
            p2: PokerHandFactory.GivenHighCard(FaceCard.Jack));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                new(new[] {new Card(FaceCard.King, Suit.Hearts)})
            }),
            p2: PokerHandFactory.GivenHighCard(FaceCard.Queen));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Queen, Suit.Spades)}),
                new(new[] {new Card(FaceCard.King, Suit.Hearts)})
            }),
            p2: PokerHandFactory.GivenHighCard(FaceCard.Ace));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenHighCard(FaceCard.Ace)
        );

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenHighCard(FaceCard.Nine));
    }
}