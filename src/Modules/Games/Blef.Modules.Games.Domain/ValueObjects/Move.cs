using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record Move(PlayerId Player, Order Order)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
    public Order Order { get; } = Order ?? throw new ArgumentNullException(nameof(Order));
}