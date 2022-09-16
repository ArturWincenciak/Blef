namespace Blef.Shared.Abstractions.Queries;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery<TResult>
    where TResult : IQueryResult
{
    Task<TResult> HandleAsync(TQuery query);
}