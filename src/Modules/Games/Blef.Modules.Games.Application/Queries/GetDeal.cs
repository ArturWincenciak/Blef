using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetDeal(Guid GameId, int DealNumber) : IQuery<GetDeal.Result>
{
    [UsedImplicitly]
    public sealed record Result(
        IReadOnlyCollection<Player> Players,
        IReadOnlyCollection<Bid> Bids,
        DealResolution DealResolution) : IQueryResult;

    // todo: create strict object of PokerHand instead of string
    // currently the poker hand for instance can return value like that: "PokerHand: two-pairs:ten,nine"
    [UsedImplicitly]
    public sealed record Bid(int Order, Guid PlayerId, string PokerHand);

    [UsedImplicitly]
    public sealed record Player(Guid PlayerId, IReadOnlyCollection<Card> Hand);

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);

    [UsedImplicitly]
    public sealed record DealResolution(Guid CheckingPlayerId, Guid LooserPlayerId);
}