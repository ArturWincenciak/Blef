using System.Runtime.CompilerServices;
using Blef.Modules.Game.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Bootstrapper")]

namespace Blef.Modules.Game.Api;

internal static class Extensions
{
    public static IServiceCollection AddGame(this IServiceCollection services) =>
        services.AddInfrastructure();
}
