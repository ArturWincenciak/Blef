﻿using System.Reflection;
using Blef.Shared.Abstractions.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace Blef.Shared.Infrastructure.Commands;

internal static class Extensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services, IEnumerable<Assembly> assemblies)
    {
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        services.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<,>))
                .WithoutAttribute<CommandHandlerDecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.Scan(typeSourceSelector => typeSourceSelector.FromAssemblies(assemblies)
            .AddClasses(filter => filter.AssignableTo(typeof(ICommandHandler<>))
                .WithoutAttribute<CommandHandlerDecoratorAttribute>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }
}