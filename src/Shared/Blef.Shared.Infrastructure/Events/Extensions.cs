using System.Reflection;
using Blef.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddDomainEvents(
        this IServiceCollection services, IEnumerable<Assembly> assemblies) =>
        services
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
}