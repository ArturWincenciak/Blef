using Blef.Modules.Games.Domain.Model;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetDeal(GameId Game, DealNumber Deal) : IQuery<GetDeal.Result>
{
    [UsedImplicitly]
    public sealed record Result(
        IEnumerable<Player> Players,
        IEnumerable<Bid> Bids,
        DealResolution DealResolution) : IQueryResult;

    [UsedImplicitly]
    public sealed record Bid(int Order, Guid PlayerId, string PokerHand);

    [UsedImplicitly]
    public sealed record Player(Guid PlayerId, IEnumerable<Card> Hand);

    [UsedImplicitly]
    public sealed record Card(string FaceCard, string Suit);

    public sealed record DealResolution(Guid CheckingPlayerId, Guid LooserPlayerId);
}