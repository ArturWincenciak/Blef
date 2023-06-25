namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids;

public sealed record HighCardBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"high-card:{FaceCard.ToString().ToLower()}";
}