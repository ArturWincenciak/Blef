using System.ComponentModel.DataAnnotations;
using Blef.Shared.Infrastructure.Api.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games.Queries;

public sealed record GetDealFlowQuery(
    [FromRoute] [NotEmptyGuid] Guid GameId,
    [FromRoute] [Range(minimum: 1, Int32.MaxValue)] int DealNumber)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/deals/{{{nameof(DealNumber)}:int}}";
}
