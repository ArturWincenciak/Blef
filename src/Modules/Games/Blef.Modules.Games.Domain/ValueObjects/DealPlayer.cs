using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class DealPlayer
{
    public PlayerId PlayerId { get; }
    public IEnumerable<Card> Cards { get; }

    public DealPlayer(PlayerId playerId, IEnumerable<Card> cards)
    {
        // todo: validate if all cards are unique
        // todo: validate if player has at least one card
        // todo: validate if player has no more five cards

        PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
        Cards = cards ?? throw new ArgumentNullException(nameof(cards));
    }
}