namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record RoyalFlushBidPayload(Suit Suit) : BidPayload
{
    public override string Serialize() =>
        $"royal-flush:{Suit.ToString().ToLower()}";
}