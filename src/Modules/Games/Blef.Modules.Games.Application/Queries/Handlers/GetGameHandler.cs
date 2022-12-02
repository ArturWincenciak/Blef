using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetGameHandler : IQueryHandler<GetGameFlow, GetGameFlow.Result>
{
    private readonly IGamesRepository _games;

    public GetGameHandler(IGamesRepository games)
    {
        _games = games;
    }

    public Task<GetGameFlow.Result> Handle(GetGameFlow query)
    {
        var game = _games.Get(query.GameId);
        var gameFlow = game.GetFlow();
        var bidFlow = gameFlow.Bids
            .Select(bid => new GetGameFlow.PlayerBid(
                Order: bid.Order, 
                PlayerId: bid.PlayerId, 
                Bid: bid.Bid))
            .ToArray();
        
        var result = new GetGameFlow.Result(bidFlow, gameFlow.CheckingPlayerId, gameFlow.LooserPlayerId);
        return Task.FromResult(result);
    }
}