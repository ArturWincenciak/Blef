namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record LowStraightBidPayload : BidPayload
{
    public override string Serialize() =>
        "low-straight";
}
