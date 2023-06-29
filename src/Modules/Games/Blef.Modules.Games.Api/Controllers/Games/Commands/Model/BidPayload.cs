namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal abstract record BidPayload
{
    public abstract string Serialize();
}
