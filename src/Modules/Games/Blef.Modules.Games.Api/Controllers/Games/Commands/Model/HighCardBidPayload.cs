namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record HighCardBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"high-card:{FaceCard.ToString().ToLower()}";
}
