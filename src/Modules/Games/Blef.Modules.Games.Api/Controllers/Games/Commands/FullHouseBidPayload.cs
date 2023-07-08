using Blef.Modules.Games.Api.Controllers.Games.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

[FullHouseUniqueCards]
internal sealed record FullHouseBidPayload(FaceCard ThreeOfAKind, FaceCard Pair) : BidPayload
{
    public override string Serialize() =>
        $"full-house:{ThreeOfAKind.ToString().ToLower()},{Pair.ToString().ToLower()}";
}