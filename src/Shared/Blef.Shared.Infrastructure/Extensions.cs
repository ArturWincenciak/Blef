using System.Runtime.CompilerServices;
using System.Text.Json;
using Blef.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
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
                var disabledModules = DetectDisabledModules(services);
                manager.AddOnlyNotDisabledModuleParts(disabledModules);
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

    private static IEnumerable<string> DetectDisabledModules(IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (false == key.Contains(":module:enabled"))
                continue;

            if (false == bool.Parse(value))
            {
                var splitKey = key.Split(":");
                var moduleName = splitKey[0];
                disabledModules.Add(moduleName);
            }
        }

        return disabledModules;
    }

    private static ApplicationPartManager AddOnlyNotDisabledModuleParts(this ApplicationPartManager manager,
        IEnumerable<string> disabledModules)
    {
        var removedParts = new List<ApplicationPart>();
        foreach (var disabledModule in disabledModules)
        {
            var parts = manager.ApplicationParts
                .Where(applicationPart => applicationPart.Name.Contains(disabledModule,
                    StringComparison.InvariantCultureIgnoreCase));

            removedParts.AddRange(parts);
        }

        foreach (var part in removedParts)
            manager.ApplicationParts.Remove(part);

        manager.FeatureProviders.Add(new InternalControllerFeatureProvider());

        return manager;
    }
}