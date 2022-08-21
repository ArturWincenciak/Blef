using System.Reflection;
using Blef.Shared.Abstractions.Modules;

namespace Blef.Bootstrapper;

internal static class ModuleLoader
{
    public static IEnumerable<Assembly> LoadAssemblies(IConfiguration configuration)
    {
        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        var loadedAssembliesFiles = loadedAssemblies
            .Where(x => false == x.IsDynamic)
            .Select(x => x.Location)
            .ToList();

        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var baseDirDllFiles = Directory.GetFiles(baseDir, "*.dll");

        var notYetLoadedDllFiles = baseDirDllFiles
            .Where(file => false == loadedAssembliesFiles.Contains(file, StringComparer.InvariantCultureIgnoreCase));

        const string modulePartPrefix = "Blef.Modules.";
        var modulePartDllFiles = notYetLoadedDllFiles
            .Where(file => file.Contains(modulePartPrefix));

        foreach (var modulePartDll in modulePartDllFiles)
        {
            var moduleNamePostfix = modulePartDll.Split(modulePartPrefix)[1];
            var moduleName = moduleNamePostfix.Split(".")[0].ToLowerInvariant();

            var enabled = configuration.GetValue<bool>($"{moduleName}:module:enabled");
            if (enabled)
            {
                var assemblyName = AssemblyName.GetAssemblyName(modulePartDll);
                var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                loadedAssemblies.Add(assembly);
            }
        }

        return loadedAssemblies;
    }

    public static IEnumerable<IModule> LoadModules(IEnumerable<Assembly> assemblies)
    {
        return Array.Empty<IModule>();
    }
}