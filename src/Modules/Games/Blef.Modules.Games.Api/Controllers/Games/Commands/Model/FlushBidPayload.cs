namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record FlushBidPayload(Suit Suit) : BidPayload
{
    public override string Serialize() =>
        $"flush:{Suit.ToString().ToLower()}";
}
