﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record RoyalFlushBidsRoute(Guid GameId, Guid PlayerId) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/royal-flush";
}