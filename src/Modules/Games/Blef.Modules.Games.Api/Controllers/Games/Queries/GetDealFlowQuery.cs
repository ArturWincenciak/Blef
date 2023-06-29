using System.ComponentModel.DataAnnotations;
using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games.Queries;

internal sealed record GetDealFlowQuery(
    [FromRoute] [NotEmptyGuid] Guid GameId,
    [FromRoute]
    [Range(minimum: 1, int.MaxValue)]
    int DealNumber)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/deals/{{{nameof(DealNumber)}:int}}";

    public static string Path(Guid gameId, int dealNumber) =>
        $"gameplays/{gameId}/deals/{dealNumber}";
}
