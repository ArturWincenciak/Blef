using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record NextDealPlayer(PlayerId PlayerId, CardsAmount CardsAmount)
{
    public PlayerId PlayerId { get; } = PlayerId ?? throw new ArgumentNullException(nameof(PlayerId));
    public CardsAmount CardsAmount { get; } = CardsAmount ?? throw new ArgumentNullException(nameof(CardsAmount));
}