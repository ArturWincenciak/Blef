﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record HighStraightBidsRoute(Guid GameId, Guid PlayerId) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/high-straight";
}
