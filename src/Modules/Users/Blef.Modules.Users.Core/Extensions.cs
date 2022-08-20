﻿using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo(assemblyName: "Blef.Modules.Users.Api")]

namespace Blef.Modules.Users.Core;

internal static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services;
}