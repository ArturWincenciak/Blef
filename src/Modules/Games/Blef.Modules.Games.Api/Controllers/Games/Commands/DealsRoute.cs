﻿using Blef.Shared.Infrastructure.Api.Validation;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

public sealed record DealsRoute(
    [NotEmptyGuid] Guid GameId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/deals";
}