using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Game.Application")]

namespace Blef.Modules.Game.Domain;

internal static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services) =>
        services;
}