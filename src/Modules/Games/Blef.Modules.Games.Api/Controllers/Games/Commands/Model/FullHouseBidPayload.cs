using Blef.Modules.Games.Api.Controllers.Games.Commands.Validators;

namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Model;

[FullHouseUniqueCards]
public sealed record FullHouseBidPayload(FaceCard ThreeOfAKind, FaceCard Pair) : BidPayload
{
    public override string Serialize() =>
        $"full-house:{ThreeOfAKind.ToString().ToLower()},{Pair.ToString().ToLower()}";
}
