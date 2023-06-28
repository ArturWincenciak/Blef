namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record StraightFlushBidPayload(Suit Suit) : BidPayload
{
    public override string Serialize() =>
        $"straight-flush:{Suit.ToString().ToLower()}";
}
