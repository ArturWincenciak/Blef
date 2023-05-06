namespace Blef.Modules.Games.Domain.Model;

internal sealed record Move(PlayerId Player, Order Order)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
    public Order Order { get; } = Order ?? throw new ArgumentNullException(nameof(Order));
}