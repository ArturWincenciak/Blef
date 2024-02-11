using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class HighCardIsOnTheTableCases : TheoryData<Table, PokerHand>
{
    public HighCardIsOnTheTableCases()
    {
        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                new(new[] {new Card(FaceCard.King, Suit.Diamonds)})
            }),
            PokerHandFactory.GivenHighCard(FaceCard.Ace));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Ace, Suit.Clubs)}),
                new(new[] {new Card(FaceCard.King, Suit.Hearts)})
            }),
            PokerHandFactory.GivenHighCard(FaceCard.King));

        Add(TableCases.GivenTable(new Hand[]
            {
                new(new[] {new Card(FaceCard.Queen, Suit.Spades)}),
                new(new[] {new Card(FaceCard.King, Suit.Hearts)})
            }),
            PokerHandFactory.GivenHighCard(FaceCard.Queen));

        Add(TableCases.GetHighestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenHighCard(FaceCard.Ace));

        Add(TableCases.GetLowestMaxCardsForFourPlayers(),
            PokerHandFactory.GivenHighCard(FaceCard.Nine));
    }
}