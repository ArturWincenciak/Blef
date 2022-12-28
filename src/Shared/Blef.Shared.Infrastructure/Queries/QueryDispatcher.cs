using Blef.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Blef.Shared.Infrastructure.Queries;

internal class QueryDispatcher : IQueryDispatcher
{
    private readonly ILogger<QueryDispatcher> _logger;
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider, ILogger<QueryDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<TQueryResult> Dispatch<TQuery, TQueryResult>(TQuery query, CancellationToken cancellation)
        where TQuery : IQuery<TQueryResult>
        where TQueryResult : IQueryResult
    {
        try
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TQueryResult>>();
            return await handler.Handle(query);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{@Query}", query);
            throw;
        }
    }
}