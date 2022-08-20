using System.Runtime.CompilerServices;
using Blef.Modules.Game.Application;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Game.Api")]

namespace Blef.Modules.Game.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddApplication();
}