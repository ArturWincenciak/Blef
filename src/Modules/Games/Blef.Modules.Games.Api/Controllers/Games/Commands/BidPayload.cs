namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal abstract record BidPayload
{
    public abstract string Serialize();
}
