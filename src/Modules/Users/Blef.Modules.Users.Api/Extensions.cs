using Blef.Modules.Users.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Modules.Users.Api;

internal static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services) =>
        services.AddCore();
}