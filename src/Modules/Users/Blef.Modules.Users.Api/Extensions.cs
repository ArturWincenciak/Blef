using Blef.Modules.Users.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Modules.Users.Api;

internal static class Extensions
{
    internal static IServiceCollection AddUsers(this IServiceCollection services) =>
        services.AddCore();
}