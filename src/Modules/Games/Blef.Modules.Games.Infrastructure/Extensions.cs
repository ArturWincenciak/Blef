using System.Runtime.CompilerServices;
using Blef.Modules.Games.Application;
using Blef.Modules.Games.Application.Repositories;
using Blef.Modules.Games.Domain.Repositories;
using Blef.Modules.Games.Infrastructure.Repositories.InMemory;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Api")]

namespace Blef.Modules.Games.Infrastructure;

internal static class Extensions
{
    internal static void AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSingleton<IGamesRepository, GamesRepository>()
            .AddSingleton<IGameplaysRepository, GameplaysRepository>()
            .AddApplication();
}