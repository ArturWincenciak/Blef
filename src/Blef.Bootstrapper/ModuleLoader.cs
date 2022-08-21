using System.Reflection;
using Blef.Shared.Abstractions.Modules;

namespace Blef.Bootstrapper;

internal static class ModuleLoader
{
    public static IEnumerable<Assembly> LoadAssemblies(IConfiguration configuration)
    {
        return Array.Empty<Assembly>();
    }

    public static IEnumerable<IModule> LoadModules(IEnumerable<Assembly> assemblies)
    {
        return Array.Empty<IModule>();
    }
}