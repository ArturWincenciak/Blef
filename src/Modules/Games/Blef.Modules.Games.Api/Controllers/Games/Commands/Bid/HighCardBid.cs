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

public sealed record PairBidRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/bids/pair";
}

public sealed record PairBidPayload(FaceCard FaceCard)
{
    public string Serialize() =>
        $"pair:{FaceCard.ToString().ToLower()}";
}

public sealed record TwoPairsBidRoute(
    [NotEmptyGuid] Guid GameId,
    [NotEmptyGuid] Guid PlayerId)
{
    public const string ROUTE = $"{{{nameof(GameId)}:Guid}}/players/{{{nameof(PlayerId)}:Guid}}/bids/two-pairs";
}

public sealed record TwoPairsBidPayload(FaceCard FirstFaceCard, FaceCard SecondFaceCard)
{
    public string Serialize() =>
        $"two-pairs:{FirstFaceCard.ToString().ToLower()},{SecondFaceCard.ToString().ToLower()}";
}
