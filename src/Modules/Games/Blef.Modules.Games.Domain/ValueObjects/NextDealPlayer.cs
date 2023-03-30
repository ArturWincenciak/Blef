using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record NextDealPlayer(PlayerId PlayerId, CardsAmount CardsAmount);