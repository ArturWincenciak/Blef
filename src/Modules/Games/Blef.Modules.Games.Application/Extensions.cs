using System.Runtime.CompilerServices;
using Blef.Modules.Games.Application.ErrorHandlingPlayground.Commands.Handlers;
using Blef.Modules.Games.Domain;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Games.Infrastructure")]

namespace Blef.Modules.Games.Application;

internal static class Extensions
{
    internal static IServiceCollection AddApplication(this IServiceCollection services) =>
        services
            .AddScoped<ErrorPlaygroundService>()
            .AddDomain();
}