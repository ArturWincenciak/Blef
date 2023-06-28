namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record FourOfAKindBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"four-of-a-kind:{FaceCard.ToString().ToLower()}";
}