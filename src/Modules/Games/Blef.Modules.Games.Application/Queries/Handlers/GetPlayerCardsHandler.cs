using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain;
using Blef.Shared.Abstractions.Queries;

internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    private readonly Games _games;

    public GetPlayerCardsHandler(Games games)
    {
        _games = games;
    }

    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query)
    {
        var game = _games.GetExistingGame(query.GameId);
        Card[] cards = game.GetCards(query.PlayerId);
        return Task.FromResult(new GetPlayerCards.Result(cards));
    }
}