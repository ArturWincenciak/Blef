using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Services;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class NewDealHandler : ICommandHandler<NewDeal, NewDeal.Result>
{
    private readonly Croupier _croupier;
    private readonly IDeckFactory _deckFactory;
    private readonly IGamesRepository _games;

    public NewDealHandler(IGamesRepository games, Croupier croupier, IDeckFactory deckFactory)
    {
        _games = games;
        _croupier = croupier;
        _deckFactory = deckFactory;
    }

    public async Task<NewDeal.Result> Handle(NewDeal command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var deal = game.NewDeal(_deckFactory, _croupier);
        var result = new NewDeal.Result(deal.Number.Number);
        return await Task.FromResult(result);
    }
}