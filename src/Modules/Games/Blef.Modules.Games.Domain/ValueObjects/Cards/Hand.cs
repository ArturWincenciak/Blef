namespace Blef.Modules.Games.Domain.ValueObjects.Cards;

internal sealed class Hand
{
    public IEnumerable<Card> Cards { get; }

    public Hand(Card[] cards)
    {
        // todo: validate if all cards are unique
        // todo: validate if here is at least one card
        // todo: validate if here is no more five cards

        Cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }
}