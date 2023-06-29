namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record HighStraightBidPayload : BidPayload
{
    public override string Serialize() =>
        "high-straight";
}