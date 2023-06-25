namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids;

public abstract record BidPayload
{
    public abstract string Serialize();
}
