using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Blef.Shared.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Events;

internal static class Extensions
{
    [SuppressMessage(category: "ReSharper", checkId: "UnusedMethodReturnValue.Global")]
    public static IServiceCollection AddDomainEvents(
        this IServiceCollection services, IReadOnlyCollection<Assembly> assemblies) =>
        services
            .AddScoped<IDomainEventDispatcher, DomainEventDispatcher>()
            .Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
                .AddClasses(filter => filter.AssignableTo(typeof(IDomainEventHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
}