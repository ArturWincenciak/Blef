using Blef.Shared.Abstractions.Modules;
using Blef.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Modules;

internal static class Extension
{
    internal static IEnumerable<string> DetectDisabledModules(this IConfiguration configuration)
    {
        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (key.Contains(":module:enabled") == false)
                continue;

            if (bool.Parse(value ?? "false") == false)
            {
                var splitKey = key.Split(":");
                var moduleName = splitKey[0];
                disabledModules.Add(moduleName);
            }
        }

        return disabledModules;
    }

    internal static void AddOnlyNotDisabledModuleParts(this ApplicationPartManager manager,
        IEnumerable<string> disabledModules)
    {
        var removedParts = new List<ApplicationPart>();
        foreach (var disabledModule in disabledModules)
        {
            var parts = manager.ApplicationParts
                .Where(applicationPart => applicationPart.Name.Contains(disabledModule,
                    StringComparison.InvariantCultureIgnoreCase));

            removedParts.AddRange(parts);
        }

        foreach (var part in removedParts)
            manager.ApplicationParts.Remove(part);

        manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
    }

    internal static IServiceCollection AddModuleInfo(this IServiceCollection services, IEnumerable<IModule> modules) =>
        services.AddSingleton(new ModuleInfoCollection(
            modules.Select(module => new ModuleInfo(module.Name, Path: $"/{module.Path}"))));
}