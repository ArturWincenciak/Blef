using Blef.Modules.Games.Api.Controllers.Games.Commands.Bids.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids;

[NotTheSameTwoPairs]
public sealed record TwoPairsBidPayload(FaceCard FirstFaceCard, FaceCard SecondFaceCard) : BidPayload
{
    public override string Serialize() =>
        $"two-pairs:{FirstFaceCard.ToString().ToLower()},{SecondFaceCard.ToString().ToLower()}";
}
