using System.Reflection;
using Blef.Shared.Abstractions.Queries;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Queries;

internal static class Extensions
{
    [UsedImplicitly]
    public static IServiceCollection AddQueries(this IServiceCollection services, IReadOnlyCollection<Assembly> assemblies) =>
        services
            .AddScoped<IQueryDispatcher, QueryDispatcher>()
            .Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
}