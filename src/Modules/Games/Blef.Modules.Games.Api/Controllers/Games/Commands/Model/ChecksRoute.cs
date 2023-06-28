using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record ChecksRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/checks";
}
