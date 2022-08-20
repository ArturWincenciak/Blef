using System.Runtime.CompilerServices;
using System.Text.Json;
using Blef.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("Blef.Bootstrapper")]

namespace Blef.Shared.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddControllers()
            .ConfigureApplicationPartManager(manager =>
            {
                manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapGet(
                pattern: "/",
                requestDelegate: async context => await context.Response.WriteAsync(
                    text: JsonSerializer.Serialize(new
                    {
                        Aplication = "Blef",
                        Description = "Card game",
                        Specification = "/swagger/index.html",
                        Repository = "https://github.com/ArturWincenciak/Blef",
                        DockerHub = "https://hub.docker.com/repository/docker/teovincent/blef",
                        Modules = new object[]
                        {
                            new
                            {
                                Module = "Games",
                                Home = "/games-module"
                            },
                            new
                            {
                                Module = "Users",
                                Home = "/users-module"
                            }
                        },
                        RequestTime = DateTime.UtcNow
                    }, new JsonSerializerOptions {WriteIndented = true})));

            endpoints.MapGet(
                pattern: "/swagger/{index?}",
                requestDelegate: async context => await context.Response.WriteAsync(
                    text: JsonSerializer.Serialize(new
                    {
                        Description = "Blef API specification to be implemented",
                        RequestTime = DateTime.UtcNow
                    }, new JsonSerializerOptions {WriteIndented = true})));
        });

        return app;
    }
}