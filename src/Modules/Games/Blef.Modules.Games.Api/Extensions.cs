using Blef.Modules.Games.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Modules.Games.Api;

internal static class Extensions
{
    public static IServiceCollection AddGames(this IServiceCollection services) =>
        services.AddInfrastructure();
}
