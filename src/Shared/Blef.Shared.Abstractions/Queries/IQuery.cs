namespace Blef.Shared.Abstractions.Queries;

public interface IQuery
{
}

public interface IQuery<TResult> : IQuery
    where TResult : IQueryResult
{
}