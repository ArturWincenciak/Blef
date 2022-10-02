namespace Blef.Shared.Abstractions.Queries;

public interface IQueryDispatcher
{
    Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery<TQueryResult>
        where TQueryResult : IQueryResult;
}