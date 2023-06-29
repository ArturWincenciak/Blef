namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record StraightFlushBidsRoute(Guid GameId, Guid PlayerId) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/straight-flush";
}