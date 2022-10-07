using Blef.Modules.Games.Application.Queries;
using Blef.Modules.Games.Domain;
using Blef.Shared.Abstractions.Queries;

internal sealed class GetPlayerCardsHandler : IQueryHandler<GetPlayerCards, GetPlayerCards.Result>
{
    public Task<GetPlayerCards.Result> Handle(GetPlayerCards query)
    {
        var card = new Card("Ace", "Diamonds");
        return Task.FromResult(new GetPlayerCards.Result(new[] { card }));
    }
}