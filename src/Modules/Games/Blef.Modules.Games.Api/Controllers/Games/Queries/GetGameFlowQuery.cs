using Blef.Shared.Infrastructure.Api.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Blef.Modules.Games.Api.Controllers.Games.Queries;

public sealed record GetGameFlowQuery(
    [FromRoute] [NotEmptyGuid] Guid GameId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}";
}
