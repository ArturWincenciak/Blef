namespace Blef.Shared.Abstractions.Queries;

public interface IQuery { }

public interface IQuery<TQueryResult> : IQuery
    where TQueryResult : IQueryResult { }