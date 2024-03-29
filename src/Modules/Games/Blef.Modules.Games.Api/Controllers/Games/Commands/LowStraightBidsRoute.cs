﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record LowStraightBidsRoute(Guid GameId, Guid PlayerId) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/low-straight";
}