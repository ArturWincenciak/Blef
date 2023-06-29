namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

internal sealed record FourOfAKindBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"four-of-a-kind:{FaceCard.ToString().ToLower()}";
}