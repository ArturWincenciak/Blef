using System.Runtime.CompilerServices;
using Blef.Modules.Games.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Bootstrapper")]

namespace Blef.Modules.Games.Api;

internal static class Extensions
{
    public static IServiceCollection AddGames(this IServiceCollection services) =>
        services.AddInfrastructure();
}
