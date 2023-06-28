using Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

public sealed record ChecksRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/checks";
}