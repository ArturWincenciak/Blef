using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games.Queries;

internal sealed record GetGameFlowQuery(
    [FromRoute] [NotEmptyGuid] Guid GameId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}";

    public static string Path(Guid gameId) =>
        $"gameplays/{gameId}";
}
