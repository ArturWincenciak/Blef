﻿using Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

public sealed record DealsRoute(
    [NotEmptyGuid] Guid GameId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/deals";
}