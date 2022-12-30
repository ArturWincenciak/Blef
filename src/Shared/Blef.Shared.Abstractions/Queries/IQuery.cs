namespace Blef.Shared.Abstractions.Queries;

public interface IQuery
{
}

// ReSharper disable once UnusedTypeParameter
public interface IQuery<TQueryResult> : IQuery
    where TQueryResult : IQueryResult
{
}