namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

public sealed record ThreeOfAKindBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"three-of-a-kind:{FaceCard.ToString().ToLower()}";
}
