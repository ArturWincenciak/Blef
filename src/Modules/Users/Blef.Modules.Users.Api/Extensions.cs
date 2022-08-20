using System.Runtime.CompilerServices;
using Blef.Modules.Users.Core;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Blef.Bootstrapper")]

namespace Blef.Modules.Users.Api;

internal static class Extensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services) =>
        services.AddCore();
}