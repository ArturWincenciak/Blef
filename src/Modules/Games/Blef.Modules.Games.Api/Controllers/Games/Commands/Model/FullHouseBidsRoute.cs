namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record FullHouseBidsRoute(Guid GameId, Guid PlayerId, FaceCard FaceCard) : BidsRoute(GameId, PlayerId)
{
    public const string ROUTE = $"{BASE_ROUTE}/full-house";
}