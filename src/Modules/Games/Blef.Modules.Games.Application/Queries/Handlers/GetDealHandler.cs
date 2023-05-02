using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetDealHandler : IQueryHandler<GetDealFlow, GetDealFlow.Result>
{
    public async Task<GetDealFlow.Result> Handle(GetDealFlow query, CancellationToken cancellation)
    {
        // todo: ...
        throw new NotImplementedException();
    }
}