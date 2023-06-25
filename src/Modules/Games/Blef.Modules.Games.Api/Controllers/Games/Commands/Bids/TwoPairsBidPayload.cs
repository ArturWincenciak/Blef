namespace Blef.Modules.Games.Api.Controllers.Games.Commands.Bids;

public sealed record TwoPairsBidPayload(FaceCard FirstFaceCard, FaceCard SecondFaceCard) : BidPayload
{
    public FaceCard FirstFaceCard { get; } = FirstFaceCard == SecondFaceCard
        ? FirstFaceCard
        : throw new Exception("First face card must be different than second face card");

    public FaceCard SecondFaceCard { get; init; } = SecondFaceCard == FirstFaceCard
        ? SecondFaceCard
        : throw new Exception("Second face card must be different than first face card");

    public override string Serialize() =>
        $"two-pairs:{FirstFaceCard.ToString().ToLower()},{SecondFaceCard.ToString().ToLower()}";
}
