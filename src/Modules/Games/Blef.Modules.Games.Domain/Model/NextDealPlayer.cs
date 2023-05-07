namespace Blef.Modules.Games.Domain.Model;

internal sealed record NextDealPlayer(PlayerId Player, CardsAmount CardsAmount, Order Order)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
    public CardsAmount CardsAmount { get; } = CardsAmount ?? throw new ArgumentNullException(nameof(CardsAmount));
    public Order Order { get; } = Order ?? throw new ArgumentNullException(nameof(Order));
}