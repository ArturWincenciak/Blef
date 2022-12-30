using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
    where TQueryResult : IQueryResult
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}