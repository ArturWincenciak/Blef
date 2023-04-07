using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed class DealPlayer
{
    public PlayerId PlayerId { get; }
    public Hand Hand { get; }

    public DealPlayer(PlayerId playerId, Hand cards)
    {
        PlayerId = playerId ?? throw new ArgumentNullException(nameof(playerId));
        Hand = cards ?? throw new ArgumentNullException(nameof(cards));
    }
}