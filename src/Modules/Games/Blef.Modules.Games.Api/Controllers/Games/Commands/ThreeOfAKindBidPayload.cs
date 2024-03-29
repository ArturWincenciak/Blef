﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record ThreeOfAKindBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"three-of-a-kind:{FaceCard.ToString().ToLower()}";
}