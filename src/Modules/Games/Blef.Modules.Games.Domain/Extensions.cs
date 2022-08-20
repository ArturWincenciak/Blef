using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Application")]

namespace Blef.Modules.Games.Domain;

internal static class Extensions
{
    public static IServiceCollection AddDomain(this IServiceCollection services) =>
        services;
}