namespace Blef.Shared.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TQueryResult>
    where TQuery : IQuery<TQueryResult>
    where TQueryResult : IQueryResult
{
    Task<TQueryResult> Handle(TQuery query, CancellationToken cancellation);
}