using Blef.Modules.Games.Domain.ValueObjects;
using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.Entities;

internal sealed class DealPlayer
{
    private readonly IEnumerable<Card> _cards;

    public PlayerId Id { get; }

    public DealPlayer(PlayerId id, IEnumerable<Card> cards)
    {
        // todo: validate if all cards are unique
        // todo: validate if player has at least one card
        // todo: validate if player has no more five cards

        Id = id;
        _cards = cards;
    }

    public IEnumerable<Card> GetCards() =>
        _cards;
}