using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Queries;

public interface IQuery
{
}

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
public interface IQuery<TQueryResult> : IQuery
    where TQueryResult : IQueryResult
{
}