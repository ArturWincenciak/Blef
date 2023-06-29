﻿using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record PlayersRoute(
    [NotEmptyGuid] Guid GameId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players";
}
