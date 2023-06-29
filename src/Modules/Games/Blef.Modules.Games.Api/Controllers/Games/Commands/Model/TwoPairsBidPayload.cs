using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

[TwoPairsUniqueCards]
internal sealed record TwoPairsBidPayload(FaceCard FirstFaceCard, FaceCard SecondFaceCard) : BidPayload
{
    public override string Serialize() =>
        $"two-pairs:{FirstFaceCard.ToString().ToLower()},{SecondFaceCard.ToString().ToLower()}";
}
