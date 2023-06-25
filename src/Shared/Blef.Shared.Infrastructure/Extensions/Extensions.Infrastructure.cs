using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Blef.Shared.Abstractions.Modules;
using Blef.Shared.Infrastructure.Commands;
using Blef.Shared.Infrastructure.Events;
using Blef.Shared.Infrastructure.Exceptions;
using Blef.Shared.Infrastructure.Modules;
using Blef.Shared.Infrastructure.Queries;
using Blef.Shared.Infrastructure.Tracing;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

[assembly: InternalsVisibleTo("Blef.Bootstrapper")]

namespace Blef.Shared.Infrastructure.Extensions;

internal static partial class Extensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration,
        IEnumerable<IModule> modules, IReadOnlyCollection<Assembly> assemblies) =>
        services
            .AddControllers(configuration)
            .AddTracing()
            .AddErrorHandling()
            .AddDevelopmentCors()
            .AddModuleInfo(modules)
            .AddSwagger()
            .AddCommands(assemblies)
            .AddQueries(assemblies)
            .AddDomainEvents(assemblies);

    public static void UseInfrastructure(this WebApplication application) =>
        application
            .ForDevelopment(app =>
                app.UseDevelopmentCors())
            .UseTracing()
            .UseErrorHandling()
            .UseRouting()
            .UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapMainInfo();
                endpoints.MapModuleInfo();
            })
            .UseSwagger()
            .UseSwaggerUI(c =>
                c.SwaggerEndpoint(
                    name: "Blef",
                    url: "/swagger/v1/swagger.json"));

    private static IServiceCollection AddControllers(this IServiceCollection services,
        IConfiguration configuration) =>
        services
            .AddControllers()
            .ConfigureApplicationPartManager(manager =>
                manager.AddOnlyNotDisabledModuleParts(configuration.DetectDisabledModules()))
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
            .Services;

    private static IServiceCollection AddSwagger(this IServiceCollection services) =>
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);
            options.SwaggerDoc(name: "v1", info: new OpenApiInfo
            {
                Title = "Blef",
                Version = "v1",
                Description = "Card Game",
                Contact = new OpenApiContact
                {
                    Name = "Artur Wincenciak",
                    Email = "artur.wincenciak@gmial.com",
                    Url = new Uri(uriString: "https://teovincent.com")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri(uriString: "https://github.com/ArturWincenciak/Blef/blob/main/LICENSE")
                }
            });
        });
}
