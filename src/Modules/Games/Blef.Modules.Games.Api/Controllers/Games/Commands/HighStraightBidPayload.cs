namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record HighStraightBidPayload : BidPayload
{
    public override string Serialize() =>
        "high-straight";
}