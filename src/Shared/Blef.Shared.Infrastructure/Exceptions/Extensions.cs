using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blef.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    private const string DEV_CORS_POLICY_NAME = nameof(DEV_CORS_POLICY_NAME);

    public static IServiceCollection AddTracing(this IServiceCollection services) =>
        services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<ExceptionToResponseMapper>();

    public static IServiceCollection AddDevelopmentCors(this IServiceCollection services) =>
        services.AddCors(options =>
            options.AddPolicy(DEV_CORS_POLICY_NAME, configurePolicy: builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

    public static IApplicationBuilder ForDevelopment(
        this WebApplication @this,
        Func<IApplicationBuilder, IApplicationBuilder> use) =>
        @this.Environment.IsDevelopment()
            ? use(@this)
            : @this;

    public static IApplicationBuilder UseDevelopmentCors(this IApplicationBuilder app) =>
        app.UseCors(DEV_CORS_POLICY_NAME);

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
        app.UseMiddleware<ErrorHandlerMiddleware>();
}