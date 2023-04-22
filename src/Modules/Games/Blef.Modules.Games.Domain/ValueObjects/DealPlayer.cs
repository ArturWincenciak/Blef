using Blef.Modules.Games.Domain.ValueObjects.Cards;
using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record DealPlayer(PlayerId PlayerId, Hand Hand, Order MoveOrder)
{
    public PlayerId PlayerId { get; } = PlayerId ?? throw new ArgumentNullException(nameof(PlayerId));
    public Hand Hand { get; } = Hand ?? throw new ArgumentNullException(nameof(Hand));
    public Order MoveOrder { get; } = MoveOrder ?? throw new ArgumentNullException(nameof(MoveOrder));
}