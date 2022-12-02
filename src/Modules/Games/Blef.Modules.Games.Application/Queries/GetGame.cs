using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetGame(Guid GameId) : IQuery<GetGame.Result>
{
    public sealed record Result(
        PlayerBid[] Bids,
        Guid CheckingPlayerId,
        Guid LooserPlayerId) : IQueryResult;

    public sealed record PlayerBid(Guid PlayerId, string Bid);
}