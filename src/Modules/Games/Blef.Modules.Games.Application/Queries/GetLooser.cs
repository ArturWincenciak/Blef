using Blef.Shared.Abstractions.Queries;

namespace Blef.Modules.Games.Application.Queries;

public sealed record GetLooser(Guid GameId) : IQuery<GetLooser.Result>
{
    public sealed record Result(Guid PlayerId) : IQueryResult;
}