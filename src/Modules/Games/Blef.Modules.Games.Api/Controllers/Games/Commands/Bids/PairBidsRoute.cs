﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids;

public sealed record PairBidsRoute(Guid GameId, Guid PlayerId) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/pair";
}