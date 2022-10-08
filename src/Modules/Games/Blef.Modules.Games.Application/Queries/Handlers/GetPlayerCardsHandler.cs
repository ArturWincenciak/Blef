using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries.Handlers;

internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly Domain.Games _games;

    public GetPlayerCardsHandler(Domain.Games games) =>
        _games = games;

    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query)
    {
        var game = _games.GetExistingGame(query.GameId);
        var cards = game.GetCards(query.PlayerId);
        return Task.FromResult(new GetPlayerCards.Result(cards));
    }
}