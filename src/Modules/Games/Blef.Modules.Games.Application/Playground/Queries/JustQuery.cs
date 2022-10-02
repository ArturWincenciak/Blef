using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Playground.Queries;

public sealed record JustQuery(
    int Id) : IQuery<JustQuery.Result>
{
    public sealed record Result(
        string JustResult,
        string JustDescription) : IQueryResult;
}