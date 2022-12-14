using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddTracing(this IServiceCollection services) =>
        services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<ExceptionToResponseMapper>();

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
        app.UseMiddleware<ErrorHandlerMiddleware>();
}