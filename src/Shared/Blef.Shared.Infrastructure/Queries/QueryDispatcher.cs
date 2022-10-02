using Blef.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Queries;

internal class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery<TQueryResult>
        where TQueryResult : IQueryResult
    {
        var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
        return await handler.Handle(query);
    }
}