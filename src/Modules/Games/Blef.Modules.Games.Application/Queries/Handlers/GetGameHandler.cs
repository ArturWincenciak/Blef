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
            .Select(bid => new GetGameFlow.GameBid(
                Order: bid.Order, 
                PlayerId: bid.PlayerId, 
                Bid: bid.Bid))
            .ToArray();

        var players = gameFlow.Players
            .Select(player => new GetGameFlow.Player(
                Id: player.PlayerId,
                Nick: player.Nick))
            .ToArray();
        
        var result = new GetGameFlow.Result(players, bidFlow, gameFlow.CheckingPlayerId, gameFlow.LooserPlayerId);
        return Task.FromResult(result);
    }
}