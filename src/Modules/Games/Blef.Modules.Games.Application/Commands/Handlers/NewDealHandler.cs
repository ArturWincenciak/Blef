using Blef.Modules.Games.Domain.Repositories;
using Blef.Shared.Abstractions.Commands;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Commands.Handlers;

[UsedImplicitly]
internal sealed class NewDealHandler : ICommandHandler<NewDeal, NewDeal.Result>
{
    private readonly IGamesRepository _games;

    public NewDealHandler(IGamesRepository games) =>
        _games = games;

    public async Task<NewDeal.Result> Handle(NewDeal command, CancellationToken cancellation)
    {
        throw new NotImplementedException();
    }
}