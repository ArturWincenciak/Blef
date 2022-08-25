using System.Text.Json;
using Blef.Shared.Infrastructure.Modules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Extensions;

internal static partial class Extensions
{
    private static void MapModuleInfo(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapGet(
            pattern: "/modules",
            requestDelegate: async context =>
            {
                var infoProvider = context.RequestServices.GetRequiredService<ModuleInfoCollection>();
                var json = JsonSerializer.Serialize(new
                {
                    infoProvider.Modules,
                    RequestTime = DateTime.UtcNow
                }, new JsonSerializerOptions {WriteIndented = true});
                await context.Response.WriteAsync(json);
            });

    private static void MapMainInfo(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapGet(
            pattern: "/",
            requestDelegate: async context => await context.Response.WriteAsync(
                text: JsonSerializer.Serialize((object) new
                {
                    Aplication = "Blef",
                    Description = "Card game",
                    Modules = "/modules",
                    Specification = "/swagger/index.html",
                    Repository = "https://github.com/ArturWincenciak/Blef",
                    DockerHub = "https://hub.docker.com/repository/docker/teovincent/blef",
                    RequestTime = DateTime.UtcNow
                }, new JsonSerializerOptions {WriteIndented = true})));
}