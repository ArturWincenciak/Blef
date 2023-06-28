﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record ThreeOfAKindBidsRoute(Guid GameId, Guid PlayerId) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/three-of-a-kind";
}
