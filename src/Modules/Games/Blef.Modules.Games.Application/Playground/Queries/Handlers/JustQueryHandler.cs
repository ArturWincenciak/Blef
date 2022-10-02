using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Playground.Queries.Handlers;

internal sealed class JustQueryHandler : IQueryHandler<JustQuery, JustQuery.Result>
{
    public async Task<JustQuery.Result> Handle(JustQuery query) =>
        await Task.FromResult(new JustQuery.Result(
            JustResult: $"Just result of ID '{query.Id}'.",
            JustDescription: $"Just description of result of ID '{query.Id}'."));
}