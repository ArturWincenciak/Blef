using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;

namespace Blef.Modules.Games.Application.Queries.Handlers;

[UsedImplicitly]
internal sealed class GetGameHandler : IQueryHandler<GetGameFlow, GetGameFlow.Result>
{

    public async Task<GetGameFlow.Result> Handle(GetGameFlow query, CancellationToken cancellation)
    {
        // todo: ...
        throw new NotImplementedException();
    }
}