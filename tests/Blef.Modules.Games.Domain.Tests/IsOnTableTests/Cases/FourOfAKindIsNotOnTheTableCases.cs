using Blef.Modules.Games.Domain.Model;
using Blef.Modules.Games.Domain.Model.PokerHands;
using Blef.Modules.Games.Domain.Tests.Extensions;

namespace Blef.Modules.Games.Domain.Tests.IsOnTableTests.Cases;

internal sealed class FourOfAKindIsNotOnTheTableCases : TheoryData<Table, PokerHand>
{
    public FourOfAKindIsNotOnTheTableCases()
    {
        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ten, Suit.Clubs),
                    new Card(FaceCard.Ten, Suit.Diamonds),
                    new Card(FaceCard.Ten, Suit.Hearts),
                    new Card(FaceCard.Nine, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenFourOfAKind(FaceCard.Ten));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Diamonds),
                    new Card(FaceCard.Ace, Suit.Hearts),
                    new Card(FaceCard.King, Suit.Clubs),
                    new Card(FaceCard.Queen, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Queen, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenFourOfAKind(FaceCard.Queen));

        Add(p1: TableCases.GivenTable(new Hand[]
            {
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Spades)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Diamonds)
                }),
                new(new[]
                {
                    new Card(FaceCard.Ace, Suit.Clubs)
                }),
                new(new[]
                {
                    new Card(FaceCard.Nine, Suit.Hearts)
                })
            }),
            p2: PokerHandFactory.GivenFourOfAKind(FaceCard.Ace));

        Add(p1: TableCases.GetLowestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenFourOfAKind(FaceCard.Ace));

        Add(p1: TableCases.GetHighestMaxCardsForFourPlayers(),
            p2: PokerHandFactory.GivenFourOfAKind(FaceCard.Nine));
    }
}