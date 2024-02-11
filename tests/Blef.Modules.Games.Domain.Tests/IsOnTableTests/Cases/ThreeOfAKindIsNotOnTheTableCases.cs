using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class ThreeOfAKindIsNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public ThreeOfAKindIsNotOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
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
            p2: PokerHandFactory.GivenThreeOfAKind(FaceCard.Ten));

        Add(p1: TableCases.GivenTable(new Hand[]
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
            p2: PokerHandFactory.GivenThreeOfAKind(FaceCard.Queen));

        Add(p1: TableCases.GivenTable(new Hand[]
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
            p2: PokerHandFactory.GivenThreeOfAKind(FaceCard.Ace));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenThreeOfAKind(FaceCard.Ace));

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenThreeOfAKind(FaceCard.Nine));
    }
}