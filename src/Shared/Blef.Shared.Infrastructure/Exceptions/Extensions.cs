using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Blef.Shared.Infrastructure.Exceptions;

internal static class Extensions
{
    public static IServiceCollection AddTracing(this IServiceCollection services) =>
        services
            .AddScoped<ErrorHandlerMiddleware>()
            .AddSingleton<ExceptionToResponseMapper>();

    public static IServiceCollection AddDevelopmentCors(this IServiceCollection services) =>
        services.AddCors(options =>
            options.AddPolicy(name: "development", builder =>
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
        app.UseCors("development");

    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder app) =>
        app.UseMiddleware<ErrorHandlerMiddleware>();
}