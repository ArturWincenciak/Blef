namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record LowStraightBidPayload : BidPayload
{
    public override string Serialize() =>
        "low-straight";
}