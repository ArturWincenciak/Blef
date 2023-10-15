using System.Runtime.CompilerServices;
using Blef.Modules.Games.Domain;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Infrastructure")]
[assembly: InternalsVisibleTo(assemblyName: "Blef.Bootstrapper.Tests")]

namespace Blef.Modules.Games.Application;

internal static class Extensions
{
    internal static void AddApplication(this IServiceCollection services) =>
        services.AddDomain();
}