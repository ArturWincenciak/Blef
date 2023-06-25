using Blef.Shared.Infrastructure.Api.Validation;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bid;

public sealed record BidsRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/bids";
}
