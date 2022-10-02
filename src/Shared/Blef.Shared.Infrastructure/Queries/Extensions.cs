using System.Reflection;
using Blef.Shared.Abstractions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Queries;

internal static class Extensions
{
    public static IServiceCollection AddQueries(this IServiceCollection services, IEnumerable<Assembly> assemblies) =>
        services
            .AddScoped<IQueryDispatcher, QueryDispatcher>()
            .Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
}