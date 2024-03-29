﻿namespace Blef.Modules.Games.Api.Controllers.Games.Commands;

internal sealed record PairBidPayload(FaceCard FaceCard) : BidPayload
{
    public override string Serialize() =>
        $"pair:{FaceCard.ToString().ToLower()}";
}