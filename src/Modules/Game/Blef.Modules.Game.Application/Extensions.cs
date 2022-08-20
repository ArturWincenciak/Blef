using System.Runtime.CompilerServices;
using Blef.Modules.Game.Domain;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Game.Infrastructure")]

namespace Blef.Modules.Game.Application;

internal static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services) =>
        services.AddDomain();
}