using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record DealPlayer(PlayerId PlayerId, Hand Hand, int MoveOrder)
{
    public PlayerId PlayerId { get; } = PlayerId ?? throw new ArgumentNullException(nameof(PlayerId));
    public Hand Hand { get; } = Hand ?? throw new ArgumentNullException(nameof(Hand));
}