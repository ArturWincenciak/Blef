using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record NextDealPlayer(PlayerId Player, CardsAmount CardsAmount, int Order)
{
    public PlayerId Player { get; } = Player ?? throw new ArgumentNullException(nameof(Player));
    public CardsAmount CardsAmount { get; } = CardsAmount ?? throw new ArgumentNullException(nameof(CardsAmount));
}