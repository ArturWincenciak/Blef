using System.Text.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blef.Shared.Infrastructure.Extensions;

internal static partial class Extensions
{
    private static void MapGetSwagger(this IEndpointRouteBuilder endpoints) =>
        endpoints.MapGet(
            pattern: "/swagger/{index?}",
            requestDelegate: async context => await context.Response.WriteAsync(
                text: JsonSerializer.Serialize(new
                {
                    Description = "Blef API specification to be implemented",
                    RequestTime = DateTime.UtcNow
                }, new JsonSerializerOptions {WriteIndented = true})));

    private static void MapGetMainHome(this IEndpointRouteBuilder endpoints) =>
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
}