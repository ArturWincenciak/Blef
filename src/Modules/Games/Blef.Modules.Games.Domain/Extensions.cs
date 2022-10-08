using System.Runtime.CompilerServices;
using Blef.Modules.Games.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Application")]

namespace Blef.Modules.Games.Domain;

internal static class Extensions
{
    internal static IServiceCollection AddDomain(this IServiceCollection services) =>
        services.AddSingleton<IGamesRepository, Games>();
}

