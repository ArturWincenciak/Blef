using System.Diagnostics.CodeAnalysis;

namespace Blef.Shared.Abstractions.Queries;

public interface IQuery
{
}

// TEMP COMMENT FOR GitHub Action TESTING
// [SuppressMessage(category: "ReSharper", checkId: "UnusedTypeParameter")]
public interface IQuery<TQueryResult> : IQuery
    where TQueryResult : IQueryResult
{
}