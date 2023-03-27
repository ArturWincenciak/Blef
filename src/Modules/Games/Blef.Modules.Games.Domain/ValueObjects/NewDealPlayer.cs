using Blef.Modules.Games.Domain.ValueObjects.Ids;

namespace Blef.Modules.Games.Domain.ValueObjects;

internal sealed record NewDealPlayer(PlayerId PlayerId, CardsAmount CardsAmount);