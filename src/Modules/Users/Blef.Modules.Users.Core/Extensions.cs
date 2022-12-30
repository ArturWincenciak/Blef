using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Users.Api")]

namespace Blef.Modules.Users.Core;

internal static class Extensions
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    internal static void AddCore(this IServiceCollection services)
    {
        // register domain's services and e.t.c here
    }
}