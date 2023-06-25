using Blef.Shared.Infrastructure.Api.Validation;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bid;

public sealed record HighCardBidRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/bids/high-card";
}

public sealed record HighCardBidPayload(FaceCard FaceCard)
{
    public string Serialize() =>
        $"high-card:{FaceCard.ToString().ToLower()}";
}
