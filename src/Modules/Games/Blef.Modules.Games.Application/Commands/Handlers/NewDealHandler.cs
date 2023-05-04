using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Events;
using Blef.Shared.Abstractions.Commands;
using Blef.Shared.Abstractions.Events;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class NewDealHandler : ICommandHandler<NewDeal, NewDeal.Result>
{
    private readonly IGamesRepository _games;
    private readonly IDomainEventDispatcher _domainEventDispatcher;

    public NewDealHandler(IGamesRepository games, IDomainEventDispatcher domainEventDispatcher)
    {
        _games = games;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task<NewDeal.Result> Handle(NewDeal command, CancellationToken cancellation)
    {
        var game = _games.Get(command.GameId);
        var newDealStarted = game.StartFirstDeal();
        await _domainEventDispatcher.Dispatch(newDealStarted, cancellation);
        return new NewDeal.Result(newDealStarted.Deal.Number);

        // todo: return in header next possible actions
    }
}