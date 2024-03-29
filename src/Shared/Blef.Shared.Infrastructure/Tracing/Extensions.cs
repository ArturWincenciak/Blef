using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Tracing;

internal static class Extensions
{
    public static IServiceCollection AddTraceing(this IServiceCollection services) =>
        services
            .AddScoped<TraceMiddleware>();

    public static IApplicationBuilder UseTracing(this IApplicationBuilder app) =>
        app.UseMiddleware<TraceMiddleware>();
}