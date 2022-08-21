using System.Runtime.CompilerServices;
using Blef.Modules.Games.Application;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Api")]

namespace Blef.Modules.Games.Infrastructure;

internal static class Extensions
{
    internal static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddApplication();
}