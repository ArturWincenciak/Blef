using Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids;

public abstract record BidsRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    protected const string BASE_ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/bids";
}
