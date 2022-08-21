using System.Runtime.CompilerServices;
using Blef.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

[assembly: InternalsVisibleTo("Blef.Bootstrapper")]

namespace Blef.Shared.Infrastructure.Extensions;

internal static partial class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddControllers()
            .ConfigureApplicationPartManager(manager =>
                manager.AddOnlyNotDisabledModuleParts(DetectDisabledModules(configuration)))
            .Services;

    public static IApplicationBuilder UseInfrastructure(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGetMainHome();
            endpoints.MapGetSwagger();
        });

        return app;
    }

    private static IEnumerable<string> DetectDisabledModules(IConfiguration configuration)
    {
        var disabledModules = new List<string>();
        foreach (var (key, value) in configuration.AsEnumerable())
        {
            if (false == key.Contains(":module:enabled"))
                continue;

            if (false == bool.Parse(value))
            {
                var splitKey = key.Split(":");
                var moduleName = splitKey[0];
                disabledModules.Add(moduleName);
            }
        }

        return disabledModules;
    }

    private static ApplicationPartManager AddOnlyNotDisabledModuleParts(this ApplicationPartManager manager,
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

        return manager;
    }
}